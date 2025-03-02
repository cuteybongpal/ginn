using System.Xml.Serialization;
using UnityEngine;

public interface ISubject
{
    public void Add(IObserver observer);
    public void Remove(IObserver observer);
    public void Notify(GameEventType type);
}
public interface IObserver
{
    public void Notify(GameEventType type);
}
public enum GameEventType
{
    PlayerMoved,
    PlayerDied
}