//------------------------------------------------------------------------------
// <copyright file="DownloadWrapper.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation
// </copyright>
//------------------------------------------------------------------------------
namespace DMLibTest
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure.Storage.DataMovement;

    internal class DownloadWrapper : DMLibWrapper
    {
        public DownloadWrapper()
        {

        }

        protected override Task DoTransferImp(TransferItem item)
        {
            return this.Download(item.SourceObject, item);
        }

        private Task Download(dynamic sourceObject, TransferItem item)
        {
            DownloadOptions downloadOptions = item.Options as DownloadOptions;
            TransferContext transferContext = item.TransferContext;
            CancellationToken cancellationToken = item.CancellationToken;
            string destPath = item.DestObject as string;
            Stream destStream = item.DestObject as Stream;

            if (cancellationToken != null && cancellationToken != CancellationToken.None)
            {
                if (destPath != null)
                {
                    return TransferManager.DownloadAsync(sourceObject, destPath, downloadOptions, transferContext, cancellationToken);
                }
                else
                {
                    return TransferManager.DownloadAsync(sourceObject, destStream, downloadOptions, transferContext, cancellationToken);
                }
            }
            else if (transferContext != null || downloadOptions != null)
            {
                if (destPath != null)
                {
                    return TransferManager.DownloadAsync(sourceObject, destPath, downloadOptions, transferContext);
                }
                else
                {
                    return TransferManager.DownloadAsync(sourceObject, destStream, downloadOptions, transferContext);
                }
            }
            else
            {
                if (destPath != null)
                {
                    return TransferManager.DownloadAsync(sourceObject, destPath);
                }
                else
                {
                    return TransferManager.DownloadAsync(sourceObject, destStream);
                }
            }
        }
    }
}
