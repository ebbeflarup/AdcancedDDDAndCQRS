﻿namespace Restaurant.Handlers
{
    public interface IHandle
    { }

    public interface IHandle<in T> :IHandle
    {
        void Handle(T orderPlaced);
    }
}