import os
import shutil
import subprocess

import builder
import consts


def try_begin_analyze(key, path):
    args = ["dotnet", "/home/msedlyarskiy/sonar/SonarScanner.MSBuild.dll",
            "begin", f"/k:{key}"]
    process = subprocess.run(args, stdout=subprocess.PIPE)
    if process.returncode == 0:
        print(f"{path}: {consts.OKGREEN}Start analyze{consts.ENDC}")
        return True
    print(f"{consts.FAIL}Start analysis failed{consts.ENDC}")
    return False


def try_end_analyze(path):
    args = ["dotnet", "/home/msedlyarskiy/sonar/SonarScanner.MSBuild.dll", "end"]
    if subprocess.run(args, stdout=subprocess.PIPE).returncode == 0:
        print(f"{consts.OKGREEN}End analyze{consts.ENDC}")
        return True
    print(f"{consts.FAIL}End analysis failed{consts.ENDC}")
    return False


def copy_reports(key):
    for f in os.listdir(".sonarqube/out/"):
        if f.isdigit(): 
            report_name = key + "_" + f + ".sarif"
            issues = f".sonarqube/out/{f}/Issues.json"
            if os.path.isfile(issues):
                shutil.copyfile(os.path.abspath(issues),
                                f"./reports/sonarqube/{report_name}")
    shutil.rmtree(".sonarqube")
    print(f"{consts.OKGREEN}Reports copied{consts.ENDC}")


for s in consts.solutions:
    solution_dir = "../projects/" + s
    solution_dir = solution_dir if os.path.isdir(
        solution_dir) else os.path.dirname(solution_dir)
    key = s.replace(".slnf", "").replace(".sln", "").replace("/", "_")
    print(key)
    if try_begin_analyze(key, solution_dir):
        builder.try_build(solution=s)
        if try_end_analyze(solution_dir):
           copy_reports(key)            
    print("----------")
