import os
import subprocess
import shutil

import consts

def analyze(bin_directory):
    abspath = os.path.abspath("../projects/" + bin_directory)
    command = [
        "docker", "run",
        "-v", f"{abspath}:/infersharp/binary_path",
        "--rm", "infersharp:latest",
        "/bin/bash", "-c", "./run_infersharp.sh binary_path; cp infer-out/report.sarif /infersharp/binary_path/"
    ]
    process = subprocess.run(command, stdout=subprocess.PIPE)
    if process.returncode == 0:
        file_name = \
            bin_directory.replace("/", "_").replace("_bin", "") + ".sarif"
        shutil.copyfile(f"{abspath}/report.sarif", f"./reports/{file_name}")

    return process.returncode == 0


for d in consts.bin_directories:
    print(f"Try analyze {d}...", end=" ", flush=True)
    ok = analyze(d)
    if ok:
        print(f"{consts.OKGREEN}Ok{consts.ENDC}")
    else:
        print(f"{consts.FAIL}Failed{consts.ENDC}")
    print("------------------")
