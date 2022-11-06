import subprocess

import consts


def get_build_command(solution):
    path = "../projects/" + s
    if solution == "xunit":
        return ["bash", f"{path}/build", "build"]
    elif solution == "efcore":
        return ["bash", f"{path}/build.sh"]
    else:
        return ["dotnet", "build", path]


builded = 0
for s in consts.solutions:
    print(f"Try build: {s}", end=" ", flush=True)
    args = get_build_command(s)
    process = subprocess.run(args, stdout=subprocess.PIPE)
    if process.returncode == 0:
        print(f"{consts.OKGREEN}Builded{consts.ENDC}")
        builded += 1
    else:
        print(f"{consts.FAIL}Failed{consts.ENDC}")
print("-----")
print(f"Total builded {builded}/{len(consts.solutions)} solutions")
