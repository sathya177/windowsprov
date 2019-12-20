using System.Collections.Generic;
using System.Threading.Tasks;

using Pulumi;
using Pulumi.Gcp.Compute.Inputs;
using Pulumi.Gcp.Compute.Outputs;
using Pulumi.Gcp.Storage;

class Program
{
    static Task<int> Main()
    {
        return Deployment.RunAsync(() => {

        // Create a GCP resource (Storage Bucket)
        /*  var bucket = new Bucket("my-bucket");

          // Export the DNS name of the bucket
          return new Dictionary<string, object>
          {
              { "bucketName", bucket.Url },
          };*/

        Pulumi.Gcp.Compute.InstanceArgs args = new Pulumi.Gcp.Compute.InstanceArgs();
        args.Zone = "asia-south1-c";
        args.MachineType = "n1-standard-1";
        InstanceBootDiskArgs bargs = new InstanceBootDiskArgs();

            //bbarags.Image = "windows-cloud/windows-2008-r2";
            var bbargs =  new InstanceBootDiskInitializeParamsArgs();
            bbargs.Image = "windows-cloud/windows-2008-r2";
            bargs.InitializeParams = bbargs;
            args.BootDisk = bargs;
            InstanceNetworkInterfacesArgs nargs = new InstanceNetworkInterfacesArgs();
            nargs.AccessConfigs = new InputList<InstanceNetworkInterfacesAccessConfigsArgs>();
            nargs.Network = "default";

            args.NetworkInterfaces.Add(nargs);

            var vm = new Pulumi.Gcp.Compute.Instance("windows2008", args, null);
            return new Dictionary<string, object> {
                { "vm", vm.Hostname }
            };

        });
    }
}
