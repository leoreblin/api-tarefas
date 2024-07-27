using Thunders.Tasks.Core.Errors;

namespace Thunders.Tasks.Core.Common
{
    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        public Result(TValue? value, bool success, Error error) : base(success, error)
        {
            _value = value;
        }

        public TValue Value => Success
            ? _value!
            : throw new InvalidOperationException("Operação inválida sem resultados.");

        public static implicit operator Result<TValue>(TValue? value) => Create(value);
    }
}
