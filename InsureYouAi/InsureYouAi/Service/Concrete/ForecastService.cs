using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using Org.BouncyCastle.Utilities.Collections;

namespace InsureYouAi.Service.Concrete
{
    public class PolicySalesData
    {
        public DateTime Date {  get; set; }
        public float SaleCount { get; set; }
    }

    public class PolicySalesForecast
    {
        public float[] ForecastedValues { get; set; }
        public float[] LowerBoundValues { get; set; }
        public float[] UpperBoundValues { get; set; }
    }
    public class ForecastService
    {
        private readonly MLContext _mlContext;

        public ForecastService()
        {
            _mlContext = new MLContext();
        }

        public PolicySalesForecast GetPolicySalesForecast(List<PolicySalesData> salesData, int Horizon)
        {
            var dataView = _mlContext.Data.LoadFromEnumerable(salesData);
            var count = salesData.Count;

            var pipeline = _mlContext.Forecasting.ForecastBySsa(
                outputColumnName: "ForecastedValues",
                inputColumnName: "SaleCount",
                windowSize: Math.Min(count / 2 - 1, 12), //12 Maks Değer count/2 yani
                seriesLength: count,
                trainSize: count,
                horizon: Horizon,
                confidenceLevel: 0.95f,
                confidenceLowerBoundColumn: "LowerBoundValues",
                confidenceUpperBoundColumn: "UpperBoundValues"
            );

            var model = pipeline.Fit(dataView);
            var engine = model.CreateTimeSeriesEngine<PolicySalesData, PolicySalesForecast>(_mlContext);
            return engine.Predict();
        }
    }
}
