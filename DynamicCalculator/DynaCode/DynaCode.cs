using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynamicCalculator
{
    public class DynaCode
    {
        public DynaCode(string equation)
        {
            this.Code = new string[] { formatedCode(equation) };
        }
        private string[] Code { get; set; }

        internal string formatedCode(string resultParameter)
        {
            return
                ImportAssemblies() +
                    "namespace DynaCore" +
                    "{" +
                    "   public class DynaCore" +
                    "   {" +
                    "       static public object Main()" +
                    "       {" +
                    "           dynamic result = "+ resultParameter + ";"+
                    "           return result;" +
                    "       }" +
                    "   }" +
                    "}";
        }

        public object Execute(bool withResult = false)
        {
            try
            {
                return CompileAndRun();
            }
            catch(Exception ex)
            {
                return "Równanie jest nieprawidłowe " + (withResult ? ex.Message : string.Empty);
            }
        }

        internal object CompileAndRun()
        {
            CompilerParameters compilerParameters = new CompilerParameters();

            compilerParameters.GenerateInMemory = true;
            compilerParameters.TreatWarningsAsErrors = false;
            compilerParameters.GenerateExecutable = false;
            compilerParameters.CompilerOptions = "/optimize";
            
            string[] references = ImportAssembliesArray();
            
            compilerParameters.ReferencedAssemblies.AddRange(references);

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerResults compile = provider.CompileAssemblyFromSource(compilerParameters, Code);

            if (compile.Errors.HasErrors)
            {
                string text = "Compile error: ";
                foreach (CompilerError ce in compile.Errors)
                {
                    text += "rn" + ce.ToString();
                }
                throw new Exception(text);
            }

            Module module = compile.CompiledAssembly.GetModules()[0];
            Type mt = null;
            MethodInfo methInfo = null;

            if (module != null)
            {
                mt = module.GetType("DynaCore.DynaCore");
            }

            if (mt != null)
            {
                methInfo = mt.GetMethod("Main");
            }

            if (methInfo != null)
            {
                return methInfo.Invoke(null, new object[] {});
            }
            return null;
        }

        string ImportAssemblies()
        {
            
            string result = "";
            var assa = AppDomain.CurrentDomain.GetAssemblies();
            result += "using System; ";
            result += "using System.Collections.Generic; ";
            
            return result;
        }

        string[] ImportAssembliesArray()
        {

            List<string> res = new List<string>() {
                "System.dll",
                "System.Data.dll",
                "mscorlib.dll",
                "Microsoft.CSharp.dll",
                "System.Dynamic.Runtime.dll",
                "System.Core.dll",
                "netstandard.dll",
                "System.Linq.Expressions.dll" };
            
            return res.ToArray();
        }
    }

}
