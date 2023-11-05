using System;
using Microsoft.CodeAnalysis;

namespace EventBusSourceGenerator;

public static class SyntaxNodeExtensions
{
    public static T GetParent<T>(this SyntaxNode node)
    {
        var parent = node.Parent;
        while (true)
        {
            if (parent is null)
            {
                throw new Exception();
            }

            if (parent is T t)
            {
                return t;
            }

            parent = parent.Parent;
        }
    }

    public static T? GetChild<T>(this SyntaxNode node)
    {
        var children = node.ChildNodes();

        foreach (var child in children)
        {
            if (child is T t)
            {
                return t;
            }
        }

        return default;
    }
}