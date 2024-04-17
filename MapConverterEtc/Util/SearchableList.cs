using Godot;
using System;
using System.Collections.Generic;

public partial class SearchableList<Key, Value> : ISearchableSpaceContainer<Key, Value>
{
    public List<(Key, Value)> list;

    public SearchableList()
    {
        list = new List<(Key, Value)> ();
    }

    void ISearchableSpaceContainer<Key, Value>.Add(Key key, Value value)
    {
        list.Add((key,value));
    }

    void ISearchableSpaceContainer<Key, Value>.Remove(Key key)
    {
        throw new NotImplementedException ();
        //list.RemoveAll(((Key,Value) kvpair) => kvpair.Item1 == key);
    }

    Value ISearchableSpaceContainer<Key, Value>.SearchNearest(Key target)
    {
        throw new NotImplementedException();
    }

    List<Value> ISearchableSpaceContainer<Key, Value>.SearchRange(Key min, Key max)
    {
        throw new NotImplementedException();
    }
}