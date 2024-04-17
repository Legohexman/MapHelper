
using System;
using System.Collections.Generic;

public interface ISearchableSpaceContainer<Key,Value>
{
    public List<Value> SearchRange(Key min, Key max);
    public Value SearchNearest(Key target);
    public void Add(Key key, Value value);
    public void Remove(Key key);
}