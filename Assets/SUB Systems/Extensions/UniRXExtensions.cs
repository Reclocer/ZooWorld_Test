using System;
using UniRx;
using TMPro;

public static class UniRXExtensions
{
    public static IDisposable SubscribeToTextMeshPro<T>(this IObservable<T> source, TextMeshProUGUI text)
    {
        return source.SubscribeWithState(text, (x, t) => t.text = x.ToString());
    }
}
