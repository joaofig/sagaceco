import csv
import WeeklyLogModel as wlm

with open("server-log-bad.csv", "r") as file:
    reader = csv.DictReader(file)

    model = wlm.WeeklyLogModel(0.1, 3.5)

    for rec in reader:
        period = int(rec['period'])
        value = float(rec['value'])

        if period > 32 * 2016 and model.is_outlier(period, value):
            print("Outlier: %d - %f" % (period, value))
        model.update(period, value)
