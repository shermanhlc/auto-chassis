using System;

public class MissingTomlKeyException : Exception
{
    public MissingTomlKeyException()
    {
    }

    public MissingTomlKeyException(string message)
        : base(message)
    {
    }

    public MissingTomlKeyException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class BadTomlParseException : Exception
{
    public BadTomlParseException()
    {
    }

    public BadTomlParseException(string message)
        : base(message)
    {
    }

    public BadTomlParseException(string message, Exception inner)
        : base(message, inner)
    {
    }
}