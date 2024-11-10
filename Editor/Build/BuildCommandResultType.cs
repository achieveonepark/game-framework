using System;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// 빌드 명령의 결과 타입
    /// </summary>
    public enum BuildCommandResultType
    {
        SUCCESS, // 성공
        WARNING, // 경고
        ERROR, // 에러
    }

    /// <summary>
    /// BuildCommandResultType 형식의 확장 메서드를 관리하는 클래스
    /// </summary>
    public static class BuildCommandResultTypeExt
    {
        /// <summary>
        /// UpperCamelCase 형식의 문자열을 반환합니다
        /// </summary>
        public static string ToUpperCamel(this BuildCommandResultType self)
        {
            return self switch
            {
                BuildCommandResultType.SUCCESS => "Success",
                BuildCommandResultType.WARNING => "Warning",
                BuildCommandResultType.ERROR => "Error",
                _ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
            };
        }
    }
}