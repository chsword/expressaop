using System;
using System.Runtime.Remoting.Messaging;

namespace ExpressAOP.Test
{
    public class TryCatchFilter:AOPAttribute
    {
        public override void Execute(IProcesser processer)
        {
            try
            {
                base.Execute(processer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                processer.AttachReturnMessage(
                    new ReturnMessage(
                (object)0, processer.MethodCallMessage.Args, processer.MethodCallMessage.ArgCount, processer.MethodCallMessage.LogicalCallContext, processer.MethodCallMessage)
                );
            }
      
        }
    }
}
