

namespace BO;

public static class Tools
{
    /// <summary>
    /// A class that implementats IEqualityComparer for List<int?>
    /// </summary>
    public class DistinctIntList : IEqualityComparer<List<int?>>
    {
        public bool Equals(List<int?>? x, List<int?>? y)
        {
            if (x is not null && y is not null)
                return x.SequenceEqual(y);
            if (x is null && y is null)
                return true;
            return false;
        }

        //Return sum of elements in list
        public int GetHashCode(List<int?> obj)
        {
            int sum = 0;
            foreach (int? num in obj)
            {
                sum += num ?? default(int);
            }
            return sum;
        }
    }
}
