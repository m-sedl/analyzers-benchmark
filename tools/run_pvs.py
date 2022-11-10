import os
import shutil
import subprocess

import builder
import consts


def run_pvs(s):
    solution_name = s.replace(".sln", "").replace("/", "_")
    args = ["pvs-studio-dotnet", "-t", "../projects/" +
            s, "-o", f"reports/pvs/plog/{solution_name}.json"]
    process = subprocess.run(args, stdout=subprocess.PIPE)
    print(f"Exit code: {process.returncode}")


os.environ["DOTNET_ROOT"] = "/usr/lib/dotnet/dotnet6-6.0.110"
for s in consts.solutions:
    print(f"Try analyze {s}")
    if "sln" not in s:
        print("skiped")
    else:
        run_pvs(s)
    print("-----")
