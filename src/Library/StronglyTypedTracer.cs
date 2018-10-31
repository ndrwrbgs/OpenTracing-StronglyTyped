namespace OpenTracing.Contrib.StronglyTyped
{
    using System;
    using OpenTracing.Propagation;

    public abstract class StronglyTypedTracer<TSpanBuilder, TSpanContext, TScopeManager, TSpan> : ITracer
        where TSpanBuilder : ISpanBuilder
        where TSpan : ISpan
        where TScopeManager : IScopeManager
        where TSpanContext : ISpanContext
    {
        public abstract TSpanBuilder BuildSpan(string operationName);
        public abstract void Inject<TCarrier>(TSpanContext spanContext, IFormat<TCarrier> format, TCarrier carrier);
        public abstract TSpanContext Extract<TCarrier>(IFormat<TCarrier> format, TCarrier carrier);
        public abstract TScopeManager ScopeManager { get; }
        public abstract TSpan ActiveSpan { get; }

        ISpanBuilder ITracer.BuildSpan(string operationName)
        {
            return this.BuildSpan(operationName);
        }
        
        void ITracer.Inject<TCarrier>(ISpanContext spanContext, IFormat<TCarrier> format, TCarrier carrier)
        {
            if (!(spanContext is TSpanContext))
            {
                throw new ArgumentException();
            }

            this.Inject((TSpanContext) spanContext, format, carrier);
        }
        
        ISpanContext ITracer.Extract<TCarrier>(IFormat<TCarrier> format, TCarrier carrier)
        {
            return this.Extract(format, carrier);
        }

        IScopeManager ITracer.ScopeManager => this.ScopeManager;
        
        ISpan ITracer.ActiveSpan => this.ActiveSpan;
    }
}