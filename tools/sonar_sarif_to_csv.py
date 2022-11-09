import os
import json
import csv


def report_to_list(report_name, report):
    print(report_name)
    res = []
    project = report_name.split("_")[0]
    for r in report["runs"][0]["results"]:
        location = ""
        if "locations" in r:
            resultFile = r["locations"][0]["resultFile"]
            location = resultFile["uri"] + " " + str(resultFile["region"]["startLine"]) + ":" + str(resultFile["region"]["startColumn"])
        d = {
            "project": project,
            "message": r["message"],
            "level": r["level"],
            "rule_id": r["ruleId"],
            "location": location,
            "report_name": report_name
        }
        res.append(d)
    return res

all_results = []
for f in os.listdir("reports/sonarqube"):
    path = "reports/sonarqube/" + f
    with open(path) as file:
        report = json.load(file)
    all_results += report_to_list(f, report)

keys = all_results[0].keys()
with open("reports/sonarqube.csv", "w", newline='') as f:
    dict_writer = csv.DictWriter(f, keys)
    dict_writer.writeheader()
    dict_writer.writerows(all_results)
