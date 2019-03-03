using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Runtime.Data;

namespace Jokify.Services.MachineLearning
{
    internal class JokeModelPrediction
    {
        [ColumnName(DefaultColumnNames.PredictedLabel)]
        public string Category { get; set; }
    }
}
