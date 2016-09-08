// Server log sample

import scala.io.Source

object ServerLog {
  def main(args: Array[String]): Unit = {
    val weeklyModel = new WeeklyLogModel(3.2)
    
    for(line <- Source.fromFile("server-log-bad.csv").getLines().drop(1)) {
      val pair = line.split(",")
      val record = new LogRecord(pair(0).toInt, pair(1).toDouble)
      
      if (record.period > 32 * 2016 && weeklyModel.isOutlier(record))
        println("Outlier " + record.period + " - " + record.value + " (" + weeklyModel.getValue(record) + ")")
      weeklyModel.update(record)
    }
  }
}


class LogRecord(val period: Int, val value: Double) { 
}


class WeeklyLogModel(val radius: Double) {
  private var models: Array[LinearRegressionModel] = new Array[LinearRegressionModel](2016) 
  
  for(i <- 0 to (models.length - 1)) {
    models(i) = new LinearRegressionModel
  }
  
  def update(record: LogRecord) {
    models(getIndex(record)).update(record.period, record.value)
  }
  
  def isOutlier(record: LogRecord): Boolean = {
    models(getIndex(record)).isOutlier(radius, record.period, record.value)
  }
  
  def getValue(record: LogRecord): Double = {
    models(getIndex(record)).getValue(record.period)
  }
  
  private def getIndex(record: LogRecord): Int = {
    record.period % 2016
  }
}

class LinearRegressionModel {
  private var count: Int = 0
  private var sumX: Double = 0.0
  private var sumY: Double = 0.0
  private var sumXY: Double = 0.0
  private var sumXX: Double = 0.0
  private var sumYY: Double = 0.0
  private var alpha: Double = 0.0
  private var beta: Double = 0.0
   
  def update(x: Double, y: Double) {
    sumX += x
    sumY += y
    sumXY += x * y
    sumXX += x * x
    sumYY += y * y
    count += 1
    
    val meanX = sumX / count
    val meanY = sumY / count
    val meanXY = sumXY / count
    val meanXX = sumXX / count
    
    beta = (meanXY - meanX * meanY) / (meanXX - meanX * meanX)
    alpha = meanY - beta * meanX
  }
  
  def getValue(x: Double): Double = {
    alpha + beta * x
  }
  
  def isOutlier(radius: Double, x: Double, y: Double): Boolean = {
    val yHat = alpha + beta * x
    val studentizedResidue = math.abs(y - yHat) / math.sqrt( getResidualVariance );
    studentizedResidue > radius
  }
  
  // Private stuff
  
  private def getResidualSumOfSquares(): Double = {
    beta * beta * sumXX - 2 * beta * sumXY + 2 * alpha * beta * sumX + sumYY - 2 * alpha * sumY + alpha * alpha * count
  }
  
  private def getResidualVariance(): Double = {
    getResidualSumOfSquares / (count - 2)
  }
}
