using Godot;
using System;
using System.Diagnostics;

public partial class ControlFunctionality : Node
{
    [Export]
    public NodePath ConvertButtonPath;
    private Button ConvertButton;

    [Export]
    public NodePath UpdateVisualsButtonPath;
    private Button UpdateVisualsButton;



    [Export]
    public NodePath InputPixelDiameterFieldPath;
    public LineEdit InputPixelDiameterField;

    [Export]
    public NodePath OutputPixelHeightFieldPath;
    public LineEdit OutputPixelHeightField;

    [Export]
    public NodePath InputFilePathFieldPath;
    public LineEdit InputFilePathField;

    [Export]
    public NodePath OutputFilePathFieldPath;
    public LineEdit OutputFilePathField;



    [Export]
    public NodePath InputPreviewPath;
    public TextureRect InputPreview;

    [Export]
    public NodePath OutputPreviewPath;
    public TextureRect OutputPreview;

    private Image inImg;
    private Image outImg;

    public override void _EnterTree()
    {
        base._EnterTree();

        ConvertButton = GetNode<Button>(ConvertButtonPath);
        ConvertButton.Pressed += ConvertFunction;

        UpdateVisualsButton = GetNode<Button>(UpdateVisualsButtonPath);
        UpdateVisualsButton.Pressed += UpdateVisualsFunction;

        InputPixelDiameterField = GetNode<LineEdit>(InputPixelDiameterFieldPath);
        OutputPixelHeightField = GetNode<LineEdit>(OutputPixelHeightFieldPath);

        InputFilePathField = GetNode<LineEdit>(InputFilePathFieldPath);
        OutputFilePathField = GetNode<LineEdit>(OutputFilePathFieldPath);


        InputPreview = GetNode<TextureRect>(InputPreviewPath);
        OutputPreview = GetNode<TextureRect>(OutputPreviewPath);
    }

    private void ConvertFunction()
    {


        GD.Print("Convert Clicked");
        if (InputFilePathField.Text.Length > 0)
        {
            inImg = Image.LoadFromFile(InputFilePathField.Text);
            outImg = EquirectangularFromEquiazimuthal(inImg, Convert.ToInt32(OutputPixelHeightField.Text), Convert.ToInt32(InputPixelDiameterField.Text));
        }

        if (OutputFilePathField.Text.Length > 0)
        {
            outImg.SavePng(OutputFilePathField.Text);
        }

        UpdateVisualsFunction();
    }

    private void UpdateVisualsFunction()
    {
        ImageTexture inPrevTex = ImageTexture.CreateFromImage(inImg);
        InputPreview.Texture = inPrevTex;

        ImageTexture outPrevTex = ImageTexture.CreateFromImage(outImg);
        OutputPreview.Texture = outPrevTex;
    }

    /// <summary>
    /// incomplete
    /// </summary>
    /// <param name="equiazimuthal"></param>
    /// <param name="outPixelHeight"></param>
    /// <param name="inPixelDiameter"></param>
    /// <returns></returns>
    private Image EquirectangularFromEquiazimuthal(Image equiazimuthal, int outPixelHeight, int inPixelDiameter)
    {
        //GD.PushError("Starting conversion image function");

        Image output = Image.Create(2 * outPixelHeight, outPixelHeight, false, Image.Format.Rgba8);
        Vector2 outMapCenter = new Vector2((float)outPixelHeight, (float)outPixelHeight / 2f);
        Vector2 inMapCenter = new Vector2((float)inPixelDiameter / 2f, (float)inPixelDiameter / 2f);

        Vector2I inImgSize = equiazimuthal.GetSize();
        Vector2 outImgSize = new Vector2(outPixelHeight * 2, outPixelHeight);

        //GD.PushError("Starting loop");

        for(int x = 0; x < outPixelHeight * 2; x++)
        {
            for(int y = 0; y < outPixelHeight; y++)
            {
                float outMapScaleDown = Mathf.Pi / (float)outPixelHeight;
                float inMapScaleUp = inPixelDiameter / (Mathf.Pi * 2f);



                Vector2 mappedPixel = new Vector2((float)x, (float)y);// - outMapCenter;
                //mappedPixel *= outMapScaleDown;
                mappedPixel = MapTools.SphereToEquiAzimuthalRect(MapTools.EquirectToSphere(mappedPixel, outImgSize, outMapCenter));//.Rotated(new Vector3(0f,1f,0f),Mathf.Pi/2f));
                mappedPixel += new Vector2(Mathf.Pi, Mathf.Pi);
                mappedPixel *= inMapScaleUp;

                if(mappedPixel.X >= inImgSize.X - 1 && mappedPixel.X < 1 && mappedPixel.Y >= inImgSize.Y - 1 && mappedPixel.Y < 1)
                {
                    output.SetPixel(x, y, new Color(1, 0, 0));
                    GD.Print("(" + x + ", " + y + ") pixel out of azimuthal bounds: " + inImgSize + " at " + mappedPixel);
                    continue;
                }

                GD.Print("Pixel mapped from: " + "(" + x + ", " + y + ") to " + mappedPixel);

                Vector2 blVec = new Vector2(Mathf.Floor(mappedPixel.X), Mathf.Floor(mappedPixel.Y));
                Vector2 brVec = new Vector2(Mathf.Ceil(mappedPixel.X), Mathf.Floor(mappedPixel.Y));
                Vector2 trVec = new Vector2(Mathf.Ceil(mappedPixel.X), Mathf.Ceil(mappedPixel.Y));
                Vector2 tlVec = new Vector2(Mathf.Floor(mappedPixel.X), Mathf.Ceil(mappedPixel.Y));

                Color bl = equiazimuthal.GetPixel(Mathf.FloorToInt(mappedPixel.X), Mathf.FloorToInt(mappedPixel.Y));
                Color br = equiazimuthal.GetPixel(Mathf.CeilToInt(mappedPixel.X), Mathf.FloorToInt(mappedPixel.Y));
                Color tr = equiazimuthal.GetPixel(Mathf.CeilToInt(mappedPixel.X), Mathf.CeilToInt(mappedPixel.Y));
                Color tl = equiazimuthal.GetPixel(Mathf.FloorToInt(mappedPixel.X), Mathf.CeilToInt(mappedPixel.Y));

                /*Color outPix = 0.707f * (
                    (bl / (4f * blVec.DistanceTo(mappedPixel))) + 
                    (br / (4f * brVec.DistanceTo(mappedPixel))) +
                    (tr / (4f * trVec.DistanceTo(mappedPixel))) +
                    (tl / (4f * tlVec.DistanceTo(mappedPixel)))
                    );*/

                Color outPix = bl.Lerp(tl,mappedPixel.Y - blVec.Y).Lerp(br.Lerp(tr,mappedPixel.Y - brVec.Y),mappedPixel.X-blVec.X);
                
                output.SetPixel(x,y, outPix);

                //GD.PushError("(" + x + ", " + y + ") " + outPix);
            }
        }

        return output;
    }
}
