﻿using System.Collections.Generic;

namespace Monaco
{
    public class EditorLanguages : List<string>
    {
        public EditorLanguages()
        {
            this.Add("abap");
            this.Add("aes");
            this.Add("apex");
            this.Add("azcli");
            this.Add("bat");
            this.Add("bicep");
            this.Add("c");
            this.Add("cameligo");
            this.Add("clojure");
            this.Add("coffeescript");
            this.Add("cpp");
            this.Add("csharp");
            this.Add("csp");
            this.Add("css");
            this.Add("cypher");
            this.Add("dart");
            this.Add("dockerfile");
            this.Add("ecl");
            this.Add("elixir");
            this.Add("flow9");
            this.Add("freemarker2");
            this.Add("freemarker2.tag-angle.interpolation-bracket");
            this.Add("freemarker2.tag-angle.interpolation-dollar");
            this.Add("freemarker2.tag-auto.interpolation-bracket");
            this.Add("freemarker2.tag-auto.interpolation-dollar");
            this.Add("freemarker2.tag-bracket.interpolation-bracket");
            this.Add("freemarker2.tag-bracket.interpolation-dollar");
            this.Add("fsharp");
            this.Add("go");
            this.Add("graphql");
            this.Add("handlebars");
            this.Add("hcl");
            this.Add("html");
            this.Add("ini");
            this.Add("java");
            this.Add("javascript");
            this.Add("json");
            this.Add("julia");
            this.Add("kotlin");
            this.Add("less");
            this.Add("lexon");
            this.Add("liquid");
            this.Add("lua");
            this.Add("m3");
            this.Add("markdown");
            this.Add("mips");
            this.Add("msdax");
            this.Add("mysql");
            this.Add("objective-c");
            this.Add("pascal");
            this.Add("pascaligo");
            this.Add("perl");
            this.Add("pgsql");
            this.Add("php");
            this.Add("pla");
            this.Add("plaintext");
            this.Add("postiats");
            this.Add("powerquery");
            this.Add("powershell");
            this.Add("proto");
            this.Add("pug");
            this.Add("python");
            this.Add("qsharp");
            this.Add("r");
            this.Add("razor");
            this.Add("redis");
            this.Add("redshift");
            this.Add("restructuredtext");
            this.Add("ruby");
            this.Add("rust");
            this.Add("sb");
            this.Add("scala");
            this.Add("scheme");
            this.Add("scss");
            this.Add("shell");
            this.Add("sol");
            this.Add("sparql");
            this.Add("sql");
            this.Add("st");
            this.Add("swift");
            this.Add("systemverilog");
            this.Add("tcl");
            this.Add("twig");
            this.Add("typescript");
            this.Add("vb");
            this.Add("verilog");
            this.Add("xml");
            this.Add("yaml");
        }

        public static List<string> GetLanguages()
        {
            return new EditorLanguages();
        }
    }
}
