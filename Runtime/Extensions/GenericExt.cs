using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFramework
{

    public static class GenericExt
    {

        public static bool ContainsAny<T>(this T self, IEnumerable<T> values)
        {
            return self.ContainsAny(values.ToArray());
        }


        public static bool ContainsAny<T>(this T self, params T[] values)
        {
            foreach (var n in values)
            {
                if (self.Equals(n))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 배열에서 모든 null 항목을 제거합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형. 클래스여야 합니다.</typeparam>
        /// <param name="arr">null을 제거할 배열입니다.</param>
        /// <returns>null 값이 없는 배열을 반환합니다.</returns>
        public static T[] RemoveNulls<T>(this T[] arr) where T : class
            => arr.Where(item => item is not null).ToArray();

        /// <summary>
        /// 배열 끝에 하나의 항목을 추가합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">항목을 추가할 배열입니다.</param>
        /// <param name="item">추가할 항목입니다.</param>
        /// <returns>항목이 추가된 배열을 반환합니다.</returns>
        public static T[] Add<T>(this T[] arr, T item)
            => arr.Concat(new[] { item }).ToArray();

        /// <summary>
        /// 배열의 모든 요소가 동일한지 확인합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">확인할 배열입니다.</param>
        /// <returns>모든 요소가 동일하면 true, 그렇지 않으면 false를 반환합니다.</returns>
        public static bool AllEqual<T>(this T[] arr)
            => arr.Distinct().Count() == 1;

        /// <summary>
        /// 배열에 null 값이 있는지 확인합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형이며, 참조 유형이어야 합니다.</typeparam>
        /// <param name="arr">확인할 배열입니다.</param>
        /// <returns>어떤 요소가 null이면 true, 그렇지 않으면 false를 반환합니다.</returns>
        public static bool AnyNull<T>(this T[] arr) where T : class
            => arr.Any(item => item is null);

        /// <summary>
        /// 하나 이상의 요소를 배열 끝에 추가합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">원래 배열입니다.</param>
        /// <param name="items">추가할 항목들입니다.</param>
        /// <returns>항목이 추가된 새로운 배열을 반환합니다.</returns>
        public static T[] AddRange<T>(this T[] arr, params T[] items)
            => arr.Concat(items).ToArray();

        /// <summary>
        /// 지정된 인덱스에 있는 요소를 배열에서 제거합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">원래 배열입니다.</param>
        /// <param name="index">제거할 요소의 인덱스입니다.</param>
        /// <returns>해당 요소가 제거된 새로운 배열을 반환합니다.</returns>
        public static T[] RemoveAt<T>(this T[] arr, int index)
        {
            if (index < 0 || index >= arr.Length) throw new IndexOutOfRangeException();
            return arr.Where((_, i) => i != index).ToArray();
        }

        /// <summary>
        /// 배열의 지정된 인덱스에 요소를 삽입합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">원래 배열입니다.</param>
        /// <param name="index">요소를 삽입할 인덱스입니다.</param>
        /// <param name="item">삽입할 항목입니다.</param>
        /// <returns>요소가 삽입된 새로운 배열을 반환합니다.</returns>
        public static T[] InsertAt<T>(this T[] arr, int index, T item)
        {
            if (index < 0 || index > arr.Length) throw new IndexOutOfRangeException();
            return arr.Take(index).Concat(new[] { item }).Concat(arr.Skip(index)).ToArray();
        }

        /// <summary>
        /// 배열의 요소를 무작위로 섞습니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">섞을 배열입니다.</param>
        /// <returns>요소가 섞인 새로운 배열을 반환합니다.</returns>
        public static T[] Shuffle<T>(this T[] arr)
        {
            var rng = new Random();
            return arr.OrderBy(_ => rng.Next()).ToArray();
        }

        /// <summary>
        /// 배열에 요소가 없는지 확인합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">확인할 배열입니다.</param>
        /// <returns>배열이 비어 있으면 true, 그렇지 않으면 false를 반환합니다.</returns>
        public static bool IsEmpty<T>(this T[] arr)
            => arr.Length == 0;

        /// <summary>
        /// 배열이 null이거나 요소가 없는지 확인합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">확인할 배열입니다.</param>
        /// <returns>배열이 null이거나 비어 있으면 true, 그렇지 않으면 false를 반환합니다.</returns>
        public static bool IsNullOrEmpty<T>(this T[] arr)
            => arr is null || arr.Length == 0;

        /// <summary>
        /// 지정된 인덱스의 요소를 반환하고, 인덱스가 범위를 벗어나면 기본값을 반환합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">요소를 검색할 배열입니다.</param>
        /// <param name="index">검색할 요소의 인덱스입니다.</param>
        /// <param name="defaultValue">인덱스가 범위를 벗어날 때 반환할 값입니다.</param>
        /// <returns>지정된 인덱스의 요소 또는 기본값을 반환합니다.</returns>
        public static T SafeGet<T>(this T[] arr, int index, T defaultValue = default)
            => index >= 0 && index < arr.Length ? arr[index] : defaultValue;

        /// <summary>
        /// 주어진 조건에 일치하는 모든 요소의 인덱스를 찾습니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">검색할 배열입니다.</param>
        /// <param name="match">요소를 일치시키는 조건입니다.</param>
        /// <returns>조건에 맞는 요소의 인덱스 배열을 반환합니다.</returns>
        public static int[] FindIndices<T>(this T[] arr, Predicate<T> match)
            => arr.Select((item, index) => match(item) ? index : -1)
                .Where(index => index != -1)
                .ToArray();

        /// <summary>
        /// 배열의 각 요소에 대해 지정된 동작을 실행합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">원래 배열입니다.</param>
        /// <param name="action">각 요소에 대해 실행할 동작입니다.</param>
        public static void ForEach<T>(this T[] arr, Action<T> action)
        {
            foreach (var item in arr)
            {
                action(item);
            }
        }

        /// <summary>
        /// 원래 배열을 최대 'chunkSize' 크기의 작은 배열(청크)로 나눕니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">원래 배열입니다.</param>
        /// <param name="chunkSize">각 청크의 최대 크기입니다.</param>
        /// <returns>작은 배열(청크)들의 배열을 반환합니다.</returns>
        public static T[][] Chunk<T>(this T[] arr, int chunkSize)
            => arr.Select((s, i) => new { Value = s, Index = i })
                .GroupBy(x => x.Index / chunkSize)
                .Select(grp => grp.Select(x => x.Value).ToArray())
                .ToArray();

        /// <summary>
        /// 원래 배열의 깊은 복사본을 만듭니다.
        /// 주의: 배열의 요소는 ICloneable 인터페이스를 구현해야 합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형이며, ICloneable을 구현해야 합니다.</typeparam>
        /// <param name="arr">원래 배열입니다.</param>
        /// <returns>원래 배열의 깊은 복사본을 반환합니다.</returns>
        public static T[] DeepCopy<T>(this T[] arr) where T : ICloneable
            => arr.Select(item => (T)item.Clone()).ToArray();

        /// <summary>
        /// 지정된 크기로 배열을 조정하고, 기존 요소를 유지합니다.
        /// 초과되는 요소는 잘라내고, 추가 공간은 기본값으로 채웁니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">원래 배열입니다.</param>
        /// <param name="newSize">배열의 새로운 크기입니다.</param>
        /// <returns>지정된 크기로 조정된 새로운 배열을 반환합니다.</returns>
        public static T[] Resize<T>(this T[] arr, int newSize)
        {
            T[] newArr = new T[newSize];
            Array.Copy(arr, newArr, Math.Min(arr.Length, newSize));
            return newArr;
        }

        /// <summary>
        /// 원래 배열의 첫 번째 요소를 제외한 모든 요소를 포함하는 새 배열을 반환합니다.
        /// 배열이 비어 있거나 단일 요소만 포함하면 빈 배열을 반환합니다.
        /// </summary>
        /// <typeparam name="T">배열의 요소 유형입니다.</typeparam>
        /// <param name="arr">원래 배열입니다.</param>
        /// <returns>첫 번째 요소가 없는 새 배열을 반환합니다.</returns>
        public static T[] Tail<T>(this T[] arr)
            => arr.Skip(1).ToArray();

        /// <summary>
        /// 배열의 첫 번째 요소를 반환합니다.
        /// 배열이 비어 있으면 해당 유형의 기본값을 반환합니다.
        /// </summary>
        /// <typeparam name="T">배열의 요소 유형입니다.</typeparam>
        /// <param name="arr">원래 배열입니다.</param>
        /// <returns>배열의 첫 번째 요소 또는 기본값을 반환합니다.</returns>
        public static T Head<T>(this T[] arr)
            => arr.FirstOrDefault();

        /// <summary>
        /// 원래 배열의 마지막 N개의 요소를 포함하는 새 배열을 반환합니다.
        /// N이 배열의 길이보다 크면 전체 배열을 반환합니다.
        /// </summary>
        /// <typeparam name="T">배열의 요소 유형입니다.</typeparam>
        /// <param name="arr">원래 배열입니다.</param>
        /// <param name="n">끝에서 가져올 요소 수입니다.</param>
        /// <returns>마지막 N개의 요소를 포함하는 새 배열을 반환합니다.</returns>
        public static T[] LastN<T>(this T[] arr, int n)
            => arr.Skip(Math.Max(0, arr.Length - n)).ToArray();

        /// <summary>
        /// 원래 배열의 처음 N개의 요소를 포함하는 새 배열을 반환합니다.
        /// N이 배열 길이보다 크면 전체 배열을 반환합니다.
        /// </summary>
        /// <typeparam name="T">배열의 요소 유형입니다.</typeparam>
        /// <param name="arr">원래 배열입니다.</param>
        /// <param name="n">처음부터 가져올 요소 수입니다.</param>
        /// <returns>처음 N개의 요소를 포함하는 새 배열을 반환합니다.</returns>
        public static T[] FirstN<T>(this T[] arr, int n)
            => arr.Take(n).ToArray();

        /// <summary>
        /// 배열에서 특정 값의 모든 인스턴스를 다른 값으로 바꿉니다.
        /// 두 항목이 같은지 확인하려면 기본 비교자를 사용합니다.
        /// </summary>
        /// <typeparam name="T">배열의 요소 유형입니다.</typeparam>
        /// <param name="arr">수정할 배열입니다.</param>
        /// <param name="oldValue">대체할 값입니다.</param>
        /// <param name="newValue">새 값입니다.</param>
        /// <returns>모든 인스턴스가 새 값으로 대체된 새 배열을 반환합니다.</returns>
        public static T[] ReplaceAll<T>(this T[] arr, T oldValue, T newValue)
            => arr.Select(item => EqualityComparer<T>.Default.Equals(item, oldValue) ? newValue : item).ToArray();

        /// <summary>
        /// 배열의 지정된 인덱스에 값을 설정합니다. 인덱스가 범위를 벗어나면 배열을 새 요소가 들어갈 수 있도록 조정합니다.
        /// 음수 인덱스는 예외를 발생시킵니다.
        /// </summary>
        /// <typeparam name="T">배열의 요소 유형입니다.</typeparam>
        /// <param name="arr">수정할 배열입니다.</param>
        /// <param name="index">값을 설정할 인덱스입니다.</param>
        /// <param name="value">설정할 값입니다.</param>
        /// <returns>지정된 인덱스에 값을 설정한 새 배열을 반환합니다.</returns>
        public static T[] SafeSet<T>(this T[] arr, int index, T value)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "인덱스는 음수가 될 수 없습니다.");
            if (index >= arr.Length)
                arr = arr.Resize(index + 1);

            arr[index] = value;
            return arr;
        }

        /// <summary>
        /// 지정된 선택자 함수에 따라 배열에서 최대 요소를 찾고 반환합니다.
        /// 배열이 비어 있으면 해당 유형의 기본값을 반환합니다.
        /// </summary>
        /// <typeparam name="T">배열의 요소 유형입니다.</typeparam>
        /// <typeparam name="TKey">비교할 키 유형입니다.</typeparam>
        /// <param name="arr">처리할 배열입니다.</param>
        /// <param name="selector">각 요소를 비교할 키로 변환하는 함수입니다.</param>
        /// <returns>키에 따라 정렬된 배열의 최대 요소를 반환합니다.</returns>
        public static T MaxBy<T, TKey>(this T[] arr, Func<T, TKey> selector) where TKey : IComparable<TKey>
            => arr.OrderByDescending(selector).FirstOrDefault();

        /// <summary>
        /// 지정된 선택자 함수에 따라 배열에서 최소 요소를 찾고 반환합니다.
        /// 배열이 비어 있으면 해당 유형의 기본값을 반환합니다.
        /// </summary>
        /// <typeparam name="T">배열의 요소 유형입니다.</typeparam>
        /// <typeparam name="TKey">비교할 키 유형입니다.</typeparam>
        /// <param name="arr">처리할 배열입니다.</param>
        /// <param name="selector">각 요소를 비교할 키로 변환하는 함수입니다.</param>
        /// <returns>키에 따라 정렬된 배열의 최소 요소를 반환합니다.</returns>
        public static T MinBy<T, TKey>(this T[] arr, Func<T, TKey> selector) where TKey : IComparable<TKey>
            => arr.OrderBy(selector).FirstOrDefault();

        /// <summary>
        /// 지정된 선택자 함수에 따라 배열에서 중복되지 않는 요소만 포함하는 배열을 반환합니다.
        /// 반환된 요소는 원래 배열에서 중복되지 않은 첫 번째 요소입니다.
        /// </summary>
        /// <typeparam name="T">배열의 요소 유형입니다.</typeparam>
        /// <typeparam name="TKey">요소를 구분할 키 유형입니다.</typeparam>
        /// <param name="arr">처리할 배열입니다.</param>
        /// <param name="selector">각 요소를 비교할 키로 변환하는 함수입니다.</param>
        /// <returns>키에 따라 중복되지 않는 요소의 배열을 반환합니다.</returns>
        public static T[] DistinctBy<T, TKey>(this T[] arr, Func<T, TKey> selector)
            => arr.GroupBy(selector).Select(grp => grp.First()).ToArray();

        /// <summary>
        /// 배열을 왼쪽으로 N번 회전시킵니다. 배열의 시작에서 이동된 요소는 끝으로 이동됩니다.
        /// 배열이 비어 있으면 빈 배열을 반환합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">회전시킬 배열입니다.</param>
        /// <param name="positions">왼쪽으로 회전할 횟수입니다.</param>
        /// <returns>왼쪽으로 N번 회전된 새 배열입니다.</returns>
        public static T[] RotateLeft<T>(this T[] arr, int positions)
        {
            if (arr.Length == 0) return arr;

            positions = positions % arr.Length;

            return arr.Skip(positions).Concat(arr.Take(positions)).ToArray();
        }

        /// <summary>
        /// 배열을 오른쪽으로 N번 회전시킵니다. 배열의 끝에서 이동된 요소는 시작으로 이동됩니다.
        /// 배열이 비어 있으면 빈 배열을 반환합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">회전시킬 배열입니다.</param>
        /// <param name="positions">오른쪽으로 회전할 횟수입니다.</param>
        /// <returns>오른쪽으로 N번 회전된 새 배열입니다.</returns>
        public static T[] RotateRight<T>(this T[] arr, int positions)
        {
            if (arr.Length == 0) return arr;
            positions = positions % arr.Length;
            return arr.Skip(arr.Length - positions).Concat(arr.Take(arr.Length - positions)).ToArray();
        }

        /// <summary>
        /// 다차원 배열 또는 배열의 배열을 단일 1차원 배열로 평탄화합니다.
        /// 결과 배열은 내부 배열의 요소가 발견된 순서대로 연결됩니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">평탄화할 배열입니다.</param>
        /// <returns>원래 배열의 모든 요소를 포함하는 1차원 배열입니다.</returns>
        public static T[] Flatten<T>(this T[][] arr)
            => arr.SelectMany(innerArray => innerArray).ToArray();

        /// <summary>
        /// 배열에서 특정 항목의 발생 횟수를 셉니다.
        /// </summary>
        /// <typeparam name="T">배열에 포함된 요소의 유형입니다.</typeparam>
        /// <param name="arr">탐색할 배열입니다.</param>
        /// <param name="item">카운트할 항목입니다.</param>
        /// <returns>배열에서 해당 항목의 발생 횟수입니다.</returns>
        public static int CountOf<T>(this T[] arr, T item) where T : IEquatable<T>
            => arr.Count(x => x.Equals(item));

        /// <summary>
        /// 배열에서 중복되지 않는 값을 반환합니다.
        /// </summary>
        /// <typeparam name="T">배열에 포함된 요소의 유형입니다.</typeparam>
        /// <param name="arr">처리할 배열입니다.</param>
        /// <returns>중복되지 않는 요소를 포함하는 배열입니다.</returns>
        public static T[] DistinctValues<T>(this T[] arr)
            => arr.Distinct().ToArray();

        /// <summary>
        /// 배열에서 가장 자주 나타나는 요소를 찾습니다.
        /// </summary>
        /// <typeparam name="T">배열에 포함된 요소의 유형입니다.</typeparam>
        /// <param name="arr">탐색할 배열입니다.</param>
        /// <returns>배열에서 가장 자주 나타나는 요소입니다. 여러 항목이 동일한 횟수로 나타나면 처음 발견된 항목을 반환합니다.</returns>
        public static T MostCommon<T>(this T[] arr) where T : IEquatable<T>
            => arr.GroupBy(x => x)
                .OrderByDescending(group => group.Count())
                .First().Key;

        /// <summary>
        /// 문자열의 서브스트링과 유사하게 배열의 일부를 반환합니다.
        /// </summary>
        /// <typeparam name="T">배열에 포함된 요소의 유형입니다.</typeparam>
        /// <param name="arr">슬라이스할 배열입니다.</param>
        /// <param name="start">0부터 시작하는 시작 위치입니다.</param>
        /// <param name="end">0부터 시작하는 끝 위치(제외)입니다. 지정하지 않으면 배열 끝까지 슬라이스됩니다.</param>
        /// <returns>배열의 일부분입니다.</returns>
        public static T[] Slice<T>(this T[] arr, int start, int? end = null)
        {
            if (start < 0 || start >= arr.Length) throw new ArgumentOutOfRangeException(nameof(start));
            if (end.HasValue && end.Value <= start) throw new ArgumentException("끝 위치는 시작 위치보다 커야 합니다.");
            if (end.HasValue && end.Value > arr.Length) throw new ArgumentOutOfRangeException(nameof(end));

            int length = (end ?? arr.Length) - start;
            return arr.Skip(start).Take(length).ToArray();
        }

        /// <summary>
        /// 두 배열의 요소를 하나의 배열로 교차하여 결합합니다. 결과 배열에서 두 배열의 요소가 교대로 나타납니다.
        /// 한 배열이 더 길면 나머지 요소가 마지막에 추가됩니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr1">교차할 첫 번째 배열입니다.</param>
        /// <param name="arr2">교차할 두 번째 배열입니다.</param>
        /// <returns>입력된 두 배열의 요소가 교차된 새 배열입니다.</returns>
        public static T[] Interleave<T>(this T[] arr1, T[] arr2)
        {
            T[] result = new T[arr1.Length + arr2.Length];
            int i = 0, j = 0, k = 0;

            while (i < arr1.Length && j < arr2.Length)
            {
                result[k++] = arr1[i++];
                result[k++] = arr2[j++];
            }

            while (i < arr1.Length)
            {
                result[k++] = arr1[i++];
            }

            while (j < arr2.Length)
            {
                result[k++] = arr2[j++];
            }

            return result;
        }

        /// <summary>
        /// 지정된 조건에 따라 배열을 세그먼트로 분할합니다. 조건이 true일 때마다 새 세그먼트가 시작됩니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">분할할 배열입니다.</param>
        /// <param name="predicate">새 세그먼트의 시작을 결정할 조건 함수입니다.</param>
        /// <returns>각 배열이 원래 배열의 세그먼트인 배열 목록입니다.</returns>
        public static List<T[]> Segment<T>(this T[] arr, Predicate<T> predicate)
        {
            var result = new List<T[]>();
            var segment = new List<T>();

            foreach (var item in arr)
            {
                if (predicate(item))
                {
                    if (segment.Any())
                    {
                        result.Add(segment.ToArray());
                        segment.Clear();
                    }
                }

                segment.Add(item);
            }

            if (segment.Any()) result.Add(segment.ToArray());

            return result;
        }

        /// <summary>
        /// 정렬된 배열에서 특정 항목을 지정된 비교기를 사용하여 이진 탐색합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">탐색할 정렬된 배열입니다.</param>
        /// <param name="item">탐색할 항목입니다.</param>
        /// <param name="comparer">요소 비교에 사용할 비교기입니다.</param>
        /// <returns>배열에서 항목의 인덱스입니다. 발견되지 않은 경우 -1을 반환합니다.</returns>
        public static int BinarySearch<T>(this T[] arr, T item, IComparer<T> comparer)
        {
            int low = 0, high = arr.Length - 1;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                int comparison = comparer.Compare(arr[mid], item);

                if (comparison == 0) return mid;

                if (comparison < 0) low = mid + 1;
                else high = mid - 1;
            }

            return -1; // 항목을 찾을 수 없음
        }

        /// <summary>
        /// 배열에서 무작위로 샘플 크기만큼 요소를 선택합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">샘플을 추출할 배열입니다.</param>
        /// <param name="sampleSize">샘플에 포함할 요소의 개수입니다.</param>
        /// <returns>원본 배열에서 무작위로 샘플링된 요소를 포함하는 배열입니다.</returns>
        public static T[] RandomSample<T>(this T[] arr, int sampleSize)
        {
            var random = new Random();
            return arr.OrderBy(x => random.Next()).Take(sampleSize).ToArray();
        }

        /// <summary>
        /// 배열의 순차적인 요소 쌍으로부터 튜플 시퀀스를 생성합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">쌍을 생성할 배열입니다.</param>
        /// <returns>배열의 순차적 요소 쌍을 포함하는 튜플의 열거 가능한 시퀀스입니다.</returns>
        public static IEnumerable<Tuple<T, T>> SequentialPairs<T>(this T[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                yield return Tuple.Create(arr[i], arr[i + 1]);
            }
        }

        /// <summary>
        /// 지정된 조건에 맞는 배열의 첫 번째와 마지막 요소를 찾습니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">탐색할 배열입니다.</param>
        /// <param name="predicate">조건에 맞는 요소를 결정할 함수입니다.</param>
        /// <returns>조건에 맞는 첫 번째 요소와 마지막 요소입니다.</returns>
        public static (T first, T last) FindFirstAndLast<T>(this T[] arr, Predicate<T> predicate)
        {
            var first = arr.FirstOrDefault(x => predicate(x));
            var last = arr.LastOrDefault(x => predicate(x));
            return (first, last);
        }

        /// <summary>
        /// 배열의 요소를 역순으로 만듭니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">역순으로 만들 배열입니다.</param>
        /// <returns>역순으로 정렬된 새 배열입니다.</returns>
        public static T[] Reverse<T>(this T[] arr)
        {
            T[] newArray = new T[arr.Length];
            Array.Copy(arr, newArray, arr.Length);
            Array.Reverse(newArray);
            return newArray;
        }

        /// <summary>
        /// 배열을 주어진 값으로 채웁니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">채울 배열입니다.</param>
        /// <param name="value">배열을 채울 값입니다.</param>
        public static void Fill<T>(this T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = value;
        }

        /// <summary>
        /// 지정된 비교기에 따라 배열이 정렬되어 있는지 확인합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">정렬 여부를 확인할 배열입니다.</param>
        /// <param name="comparer">요소를 비교할 비교기입니다. 기본 비교기를 사용하지 않은 경우 null을 전달할 수 있습니다.</param>
        /// <returns>배열이 정렬되어 있으면 true, 그렇지 않으면 false입니다.</returns>
        public static bool IsSorted<T>(this T[] arr, IComparer<T> comparer = null)
        {
            comparer ??= Comparer<T>.Default;
            for (int i = 1; i < arr.Length; i++)
                if (comparer.Compare(arr[i - 1], arr[i]) > 0)
                    return false;
            return true;
        }

        /// <summary>
        /// 배열에서 중복된 요소를 제거합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">중복을 제거할 배열입니다.</param>
        /// <returns>중복이 제거된 새 배열입니다.</returns>
        public static T[] RemoveDuplicates<T>(this T[] arr)
            => new HashSet<T>(arr).ToArray();

        /// <summary>
        /// 두 배열을 지정된 선택 함수를 사용하여 하나의 배열로 결합합니다.
        /// </summary>
        /// <typeparam name="T">첫 번째 배열의 요소 유형입니다.</typeparam>
        /// <typeparam name="TOther">두 번째 배열의 요소 유형입니다.</typeparam>
        /// <typeparam name="TResult">결과 배열의 요소 유형입니다.</typeparam>
        /// <param name="arr">결합할 첫 번째 배열입니다.</param>
        /// <param name="other">결합할 두 번째 배열입니다.</param>
        /// <param name="selector">요소 결합을 결정할 선택 함수입니다.</param>
        /// <returns>결합된 결과 배열입니다.</returns>
        public static TResult[] ZipWith<T, TOther, TResult>(this T[] arr, TOther[] other,
            Func<T, TOther, TResult> selector)
        {
            var minLength = Math.Min(arr.Length, other.Length);
            var result = new TResult[minLength];
            for (int i = 0; i < minLength; i++)
                result[i] = selector(arr[i], other[i]);
            return result;
        }

        /// <summary>
        /// 배열의 각 요소에 대해 인덱스를 제공하여 작업을 수행합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">작업을 수행할 배열입니다.</param>
        /// <param name="action">요소와 인덱스를 매개변수로 받는 작업입니다.</param>
        public static void ForEachIndexed<T>(this T[] arr, Action<T, int> action)
        {
            for (int i = 0; i < arr.Length; i++)
                action(arr[i], i);
        }

        /// <summary>
        /// 선택 함수를 기반으로 배열 요소의 합계를 계산합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">합계를 계산할 배열입니다.</param>
        /// <param name="selector">요소의 값을 선택하는 함수입니다.</param>
        /// <returns>선택된 값들의 합입니다.</returns>
        public static decimal SumBy<T>(this T[] arr, Func<T, decimal> selector)
            => arr.Sum(selector);

        /// <summary>
        /// 선택 함수를 기반으로 배열 요소의 평균을 계산합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">평균을 계산할 배열입니다.</param>
        /// <param name="selector">요소의 값을 선택하는 함수입니다.</param>
        /// <returns>선택된 값들의 평균입니다.</returns>
        public static double AverageBy<T>(this T[] arr, Func<T, double> selector)
            => arr.Average(selector);

        /// <summary>
        /// 배열을 HashSet으로 변환하여 중복을 제거하고 빠른 조회가 가능하게 만듭니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">HashSet으로 변환할 배열입니다.</param>
        /// <returns>중복이 제거된 HashSet입니다.</returns>
        public static HashSet<T> ToHashSet<T>(this T[] arr)
            => new HashSet<T>(arr);

        /// <summary>
        /// 배열의 각 요소를 선택 함수를 사용하여 변환합니다.
        /// </summary>
        /// <typeparam name="T">원본 배열의 요소 유형입니다.</typeparam>
        /// <typeparam name="TResult">변환된 요소의 유형입니다.</typeparam>
        /// <param name="arr">변환할 배열입니다.</param>
        /// <param name="selector">변환 함수를 나타내는 선택 함수입니다.</param>
        /// <returns>변환된 요소가 포함된 새 배열입니다.</returns>
        public static TResult[] Map<T, TResult>(this T[] arr, Func<T, TResult> selector)
        {
            return arr.Select(selector).ToArray();
        }

        /// <summary>
        /// 왼쪽에서 오른쪽으로 배열 요소를 누적 계산합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">누적 계산할 배열입니다.</param>
        /// <param name="func">누적 계산을 수행할 함수입니다.</param>
        /// <returns>누적 계산된 값입니다.</returns>
        public static T FoldLeft<T>(this T[] arr, Func<T, T, T> func)
        {
            return arr.Aggregate(func);
        }

        /// <summary>
        /// 배열의 모든 요소가 고유한지 확인합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">고유성을 확인할 배열입니다.</param>
        /// <returns>모든 요소가 고유하면 true, 그렇지 않으면 false입니다.</returns>
        public static bool IsUnique<T>(this T[] arr)
        {
            return arr.Distinct().Count() == arr.Length;
        }

        /// <summary>
        /// 배열의 모든 가능한 순열을 생성합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">순열을 생성할 배열입니다.</param>
        /// <returns>모든 가능한 순열을 포함하는 배열의 열거 가능한 시퀀스입니다.</returns>
        public static IEnumerable<T[]> Permute<T>(this T[] arr)
        {
            return Permute(arr, arr.Length);
        }

        /// <summary>
        /// 배열의 모든 가능한 부분 집합을 반환합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">부분 집합을 생성할 배열입니다.</param>
        /// <returns>부분 집합을 포함하는 배열의 열거 가능한 시퀀스입니다.</returns>
        public static IEnumerable<IEnumerable<T>> Subset<T>(this T[] arr)
        {
            return Enumerable.Range(0, 1 << arr.Length)
                .Select(index => arr.Where((t, i) => (index & (1 << i)) != 0));
        }

        /// <summary>
        /// 배열에 지정된 요소가 포함되어 있는지 확인합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">요소를 검색할 배열입니다.</param>
        /// <param name="item">포함 여부를 확인할 요소입니다.</param>
        /// <returns>요소가 포함되어 있으면 true, 그렇지 않으면 false입니다.</returns>
        public static bool Contains<T>(this T[] arr, T item) where T : IEquatable<T>
        {
            return arr.Any(x => x.Equals(item));
        }

        /// <summary>
        /// 배열 요소의 문자열 표현을 지정된 구분자로 결합합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">문자열로 결합할 배열입니다.</param>
        /// <param name="delimiter">요소 사이에 삽입할 구분자입니다.</param>
        /// <returns>구분자로 결합된 문자열입니다.</returns>
        public static string JoinToString<T>(this T[] arr, string delimiter)
        {
            return string.Join(delimiter, arr.Select(x => x.ToString()));
        }

        /// <summary>
        /// 조건에 맞는 요소를 찾거나 찾지 못한 경우 기본값을 반환합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">검색할 배열입니다.</param>
        /// <param name="match">조건을 나타내는 함수입니다.</param>
        /// <param name="defaultValue">조건에 맞는 요소가 없을 때 반환할 기본값입니다.</param>
        /// <returns>조건에 맞는 요소 또는 기본값입니다.</returns>
        public static T FindOrDefault<T>(this T[] arr, Predicate<T> match, T defaultValue = default)
        {
            return arr.FirstOrDefault(x => match(x)) ?? defaultValue;
        }

        /// <summary>
        /// 조건이 true인 동안 요소를 가져옵니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">요소를 가져올 배열입니다.</param>
        /// <param name="predicate">요소를 가져올 조건 함수입니다.</param>
        /// <returns>조건이 true인 동안 가져온 요소를 포함하는 배열입니다.</returns>
        public static T[] TakeWhile<T>(this T[] arr, Func<T, bool> predicate)
        {
            return arr.TakeWhile(predicate).ToArray();
        }

        /// <summary>
        /// 조건이 true인 동안 요소를 건너뜁니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <param name="arr">요소를 건너뛸 배열입니다.</param>
        /// <param name="predicate">요소를 건너뛸 조건 함수입니다.</param>
        /// <returns>조건이 true인 동안 건너뛴 후 나머지 요소를 포함하는 배열입니다.</returns>
        public static T[] SkipWhile<T>(this T[] arr, Func<T, bool> predicate)
        {
            return arr.SkipWhile(predicate).ToArray();
        }

        /// <summary>
        /// 특정 키 또는 속성을 공유하는 인접 요소를 그룹화합니다.
        /// </summary>
        /// <typeparam name="T">배열 요소의 유형입니다.</typeparam>
        /// <typeparam name="TKey">요소를 그룹화하는 키 또는 속성 유형입니다.</typeparam>
        /// <param name="arr">그룹화할 배열입니다.</param>
        /// <param name="keySelector">요소의 키를 선택하는 함수입니다.</param>
        /// <returns>인접 요소가 동일한 키로 그룹화된 그룹 컬렉션입니다.</returns>
        public static IEnumerable<IGrouping<TKey, T>> GroupBySequential<T, TKey>(this T[] arr,
            Func<T, TKey> keySelector)
            where TKey : notnull
        {
            TKey lastKey = default!;
            bool hasLastKey = false;

            return arr.GroupBy(x =>
            {
                TKey key = keySelector(x);
                if (!hasLastKey || !EqualityComparer<TKey>.Default.Equals(key, lastKey))
                {
                    lastKey = key;
                    hasLastKey = true;
                }

                return lastKey;
            });
        }

        // Permute 메서드를 위한 헬퍼 메서드
        private static IEnumerable<T[]> Permute<T>(T[] arr, int count)
        {
            if (count == 1) yield return arr;
            else
            {
                for (int i = 0; i < count; i++)
                {
                    foreach (var perm in Permute(arr, count - 1))
                        yield return perm;

                    if (count % 2 == 0)
                        (arr[i], arr[count - 1]) = (arr[count - 1], arr[i]); // 짝수 개수일 때 교환
                    else
                        (arr[0], arr[count - 1]) = (arr[count - 1], arr[0]); // 홀수 개수일 때 교환
                }
            }
        }
    }
}