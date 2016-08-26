import ExponentialMovingModel as emm

class WeeklyLogModel(object):
    """The model for a weekly log"""

    def __init__(self, weight, radius):
        self.weight = weight
        self.radius = radius
        self.models = []
        
        for i in range(2016):
            self.models.append(emm.ExponentialMovingModel(weight))

    def update(self, period, value):
        index = period % 2016
        self.models[index].update(value)

    def is_outlier(self, period, value):
        index = period % 2016
        return self.models[index].is_outlier(self.radius, value)

    def get_average(self, period):
        index = period % 2016
        return self.models[index].average

    def get_variance(self, period):
        index = period % 2016
        return self.models[index].variance

