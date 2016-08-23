#
# Generates the simulated server log
#

import csv
import numpy as np
import random

base = 750
amplitude = 500
growth = 1e-6
noise = 20

with open('server-log.csv', 'w', newline='') as csvfile:
    fieldnames = ['period', 'value']
    writer = csv.DictWriter(csvfile, fieldnames)
    writer.writeheader()

    for i in range(0, 2016 * 52):
        writer.writerow({'period': i, 'value': (base * (1 + growth)) + amplitude * np.sin( i * np.pi * 2 / 288 - np.pi / 2) + (random.random() * 2 * noise - noise) } )

