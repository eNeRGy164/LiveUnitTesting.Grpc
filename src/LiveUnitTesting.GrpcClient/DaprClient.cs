namespace LiveUnitTesting.GrpcClient
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Google.Protobuf;

    /// <summary>
    /// 
    /// </summary>
    public abstract class DaprClient : IDisposable
    {
        private bool disposed;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Method()
        {
            return this.GetHashCode();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (!this.disposed)
            {
                Dispose(disposing: true);
                this.disposed = true;
            }
        }

        /// <summary>
        /// Disposes the resources associated with the object.
        /// </summary>
        /// <param name="disposing"><c>true</c> if called by a call to the <c>Dispose</c> method; otherwise false.</param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
