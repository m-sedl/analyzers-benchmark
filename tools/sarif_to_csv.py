import os
import json
import csv


def report_to_list(report_name, report):
    res = []
    project = report_name.split("_")[0]
    for r in report["runs"][0]["results"]:
        physical_location = r["locations"][0]["physicalLocation"]
        location = physical_location["artifactLocation"]["uri"] + " " + str(physical_location["region"]["startLine"]) + ":" + str(physical_location["region"]["startColumn"])
        d = {
            "project": project,
            "message": r["message"]["text"],
            "level": r["level"],
            "rule_id": r["ruleId"],
            "location": location,
            "report_name": report_name
        }
        res.append(d)
    return res

all_results = []
for f in os.listdir("reports/infersharp"):
    path = "reports/infersharp/" + f
    with open(path) as file:
        report = json.load(file)
    all_results += report_to_list(f, report)

keys = all_results[0].keys()
with open("reports/infer.csv", "w", newline='') as f:
    dict_writer = csv.DictWriter(f, keys)
    dict_writer.writeheader()
    dict_writer.writerows(all_results)
