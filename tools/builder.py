import subprocess

import consts


def get_build_command(solution):
    path = "../projects/" + solution
    if solution == "xunit":
        return ["bash", f"{path}/build", "build"]
    elif solution == "efcore":
        return ["bash", f"{path}/build.sh"]
    elif solution == "BenchmarkDotNet":
        return ["bash", f"{path}/build.sh"]
    else:
        return ["dotnet", "build", path]


def try_build(solution):
    print(f"Try build: {solution}", end=" ", flush=True)
    args = get_build_command(solution)
    process = subprocess.run(args, stdout=subprocess.PIPE)
    if process.returncode == 0:
        print(f"{consts.OKGREEN}Builded{consts.ENDC}")
        return True
    else:
        print(f"{consts.FAIL}Failed{consts.ENDC}")
        return False


if __name__ == "__main__":
    builded = 0
    for s in consts.solutions:
        if try_build(solution=s):
            builded += 1

    print("-----")
    print(f"Total builded {builded}/{len(consts.solutions)} solutions")
