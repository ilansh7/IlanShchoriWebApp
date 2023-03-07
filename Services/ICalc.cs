using System;

namespace IlanShchoriWebApp.Services
{
    internal interface ICalc<T>
        where T : IComparable<T>
    {
        T Add(T a, T b);
        T Sub(T a, T b);
        T Mul(T a, T b);
        T Div(T a, T b);
        T CustomOper1(T a, T b);
        T CustomOper2(T a, T b);
    }
}
