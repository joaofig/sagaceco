import math

class ExponentialMovingModel(object):
    """An exponential moving model"""

    def __init__(self, weight):
        self.weight = weight
        self.average = 0.0
        self.variance = 0.0

    def update(self, value):
        if self.average == 0.0 and self.variance == 0.0:
            self.average = value
        else:
            diff = value - self.average
            incr = self.weight * diff

            self.average += incr
            self.variance = (1.0 - self.weight) * (self.variance + diff * incr)

    def is_outlier(self, radius, value):
        return math.fabs(value - self.average) > radius * math.sqrt(self.variance)
