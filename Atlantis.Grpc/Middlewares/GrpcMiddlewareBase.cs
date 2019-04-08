using System.Threading.Tasks;

namespace Atlantis.Grpc.Middlewares
{
    public abstract class GrpcMiddlewareBase
    {
        private readonly HandlerDelegateAsync _next;

        public GrpcMiddlewareBase(HandlerDelegateAsync next)
        {
            _next=next;
        }

        public virtual async Task HandleAsync(GrpcContext context)
        {
            await DoHandleAsync(context);
            if (!context.HasDone)await _next(context);
            await DoHandleResultAsync(context);
        }

        protected abstract Task DoHandleAsync(GrpcContext context);

        protected virtual Task DoHandleResultAsync(GrpcContext context)
        {
            return Task.CompletedTask;
        }
    }
}
