import os

def main():
    baseName = "Expr"
    outdir = os.path.abspath(os.path.join(os.path.dirname( __file__ ), '..', 'cslox'))

    l = ["Binary: Expr left, Token oper, Expr right", 
    "Grouping: Expr expr", 
    "Literal: object value", 
    "Unary: Token oper, Expr right"]

    defineAst(outdir, baseName, l)

def defineAst(outdir, baseName, types):
    filename = os.path.join(outdir, baseName + '.cs')

    output = []
    output.append("namespace cslox")
    output.append("{")
    output.append("")
    
    output.append("public abstract class " + baseName)
    output.append("{")
    output.append("}")
    output.append("")
    
    for t in types:
        output = defineType(t, output)

    # close namespace
    output.append("")
    output.append("}")

    with open(filename, 'w') as f:
        for line in output:
            f.write(line)
            f.write('\n')

def defineType(typestr: str, output:list) -> list:
    spl = typestr.split(":")
    name = spl[0].strip()
    args = spl[1].strip()

    output.append("public class " + name)
    output.append("{")
    indivargs = args.split(",")
    for i in indivargs:
        output.append("\t" + i.strip() + ";")
    output.append("") # blank line
    output.append("\tpublic " + name + "(" + args + ")") # constructor
    output.append("\t{")
    for i in indivargs:
        ispl = i.strip().split(' ')
        print(ispl)
        tname = ispl[0].strip()
        tvar = ispl[1].strip()
        output.append("\t\tthis." + tvar + " = " + tvar + ";")
    output.append("\t}")
    output.append("}")

    return output

if __name__ == "__main__":
    main()