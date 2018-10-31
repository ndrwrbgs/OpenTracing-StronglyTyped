namespace OpenTracing.Contrib.StronglyTyped
{
    public abstract class StronglyTypedScope<TSpan> : IScope
        where TSpan : ISpan
    {
        public abstract void Dispose();
        public abstract TSpan Span { get; }

        ISpan IScope.Span => this.Span;
    }
}