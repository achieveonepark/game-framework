using System;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace GameFramework.Editor.Build
{
    /// <summary>
    /// 플레이어를 빌드하는 명령을 관리하는 클래스
    /// </summary>
    public sealed class BuildPlayerCommand : BuildCommandBase
    {
        public override string Tag => nameof(BuildPlayerCommand);

        private readonly BuildPlayerOptions _buildPlayerOptions;

        /// <summary>
        /// 생성자
        /// </summary>
        public BuildPlayerCommand(BuildPlayerOptions buildPlayerOptions)
        {
            _buildPlayerOptions = buildPlayerOptions;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public BuildPlayerCommand
        (
            string[] levels,
            string locationPathName,
            BuildTarget target,
            BuildOptions options
        ) : this
        (
            new BuildPlayerOptions
            {
                scenes = levels,
                locationPathName = locationPathName,
                target = target,
                options = options
            }
        )
        {
        }

        /// <summary>
        /// 실행할 때 호출됩니다
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            var report = BuildPipeline.BuildPlayer(_buildPlayerOptions);
            var isSuccess = report.summary.result == BuildResult.Succeeded;
            var message = ToReportMessage(report, _buildPlayerOptions);

            return isSuccess ? Success(message) : Error(message);
        }

        /// <summary>
        /// 빌드 보고서를 메시지로 변환하여 반환합니다
        /// </summary>
        private static string ToReportMessage(BuildReport report, BuildPlayerOptions options)
        {
            var builder = new StringBuilder();
            builder.AppendLine();
            builder.AppendLine(EditorJsonUtility.ToJson(new JsonBuildPlayerOptions(options), true));
            builder.AppendLine(EditorJsonUtility.ToJson(new JsonBuildReport(report), true));
            var message = builder.ToString();
            return message;
        }

        //==============================================================================
        // 구조체
        //==============================================================================
        /// <summary>
        /// BuildPlayerOptions 형식을 JSON으로 변환할 수 있도록 하는 구조체
        /// </summary>
        [Serializable]
        private struct JsonBuildPlayerOptions
        {
            [UsedImplicitly] public string[] scenes;
            [UsedImplicitly] public string locationPathName;
            [UsedImplicitly] public string assetBundleManifestPath;
            [UsedImplicitly] public BuildTargetGroup targetGroup;
            [UsedImplicitly] public BuildTarget target;
            [UsedImplicitly] public BuildOptions options;
            [UsedImplicitly] public string[] extraScriptingDefines;

            public JsonBuildPlayerOptions(BuildPlayerOptions other)
            {
                scenes = other.scenes;
                locationPathName = other.locationPathName;
                assetBundleManifestPath = other.assetBundleManifestPath;
                targetGroup = other.targetGroup;
                target = other.target;
                options = other.options;
                extraScriptingDefines = other.extraScriptingDefines;
            }
        }

        /// <summary>
        /// BuildReport 형식을 JSON으로 변환할 수 있도록 하는 구조체
        /// </summary>
        [Serializable]
        private struct JsonBuildReport
        {
            [UsedImplicitly] public JsonBuildSummary summary;
            [UsedImplicitly] public JsonBuildStep[] steps;

            public JsonBuildReport(BuildReport other)
            {
                summary = new JsonBuildSummary(other.summary);
                steps = other.steps.Select(x => new JsonBuildStep(x)).ToArray();
            }
        }

        /// <summary>
        /// BuildSummary 형식을 JSON으로 변환할 수 있도록 하는 구조체
        /// </summary>
        [Serializable]
        private struct JsonBuildSummary
        {
            [UsedImplicitly] public string buildStartedAt;
            [UsedImplicitly] public string buildEndedAt;
            [UsedImplicitly] public string guid;
            [UsedImplicitly] public string platform;
            [UsedImplicitly] public string platformGroup;
            [UsedImplicitly] public string options;
            [UsedImplicitly] public string outputPath;
            [UsedImplicitly] public string totalSize;
            [UsedImplicitly] public string totalTime;
            [UsedImplicitly] public string totalWarnings;
            [UsedImplicitly] public string totalErrors;
            [UsedImplicitly] public string result;

            public JsonBuildSummary(BuildSummary other)
            {
                buildStartedAt = other.buildStartedAt.ToString("yyyy/MM/dd HH:mm:ss");
                guid = other.guid.ToString();
                platform = other.platform.ToString();
                platformGroup = other.platformGroup.ToString();
                options = other.options.ToString();
                outputPath = other.outputPath;
                totalSize = other.totalSize / 1024 / 1024 + " MB";
                totalTime = other.totalTime.TotalSeconds.ToString("0.0") + " 초";
                buildEndedAt = other.buildEndedAt.ToString("yyyy/MM/dd HH:mm:ss");
                totalErrors = other.totalErrors + " 개";
                totalWarnings = other.totalWarnings + " 개";
                result = other.result.ToString();
            }
        }

        /// <summary>
        /// BuildStep 형식을 JSON으로 변환할 수 있도록 하는 구조체
        /// </summary>
        [Serializable]
        private struct JsonBuildStep
        {
            [UsedImplicitly] public string name;
            [UsedImplicitly] public string duration;
            [UsedImplicitly] public JsonBuildStepMessage[] messages;
            [UsedImplicitly] public int depth;

            public JsonBuildStep(BuildStep other)
            {
                name = other.name;
                duration = other.duration.TotalSeconds.ToString("0.0") + " 초";
                messages = other.messages.Select(x => new JsonBuildStepMessage(x)).ToArray();
                depth = other.depth;
            }
        }

        /// <summary>
        /// BuildStepMessage 형식을 JSON으로 변환할 수 있도록 하는 구조체
        /// </summary>
        [Serializable]
        private struct JsonBuildStepMessage
        {
            [UsedImplicitly] public string type;
            [UsedImplicitly] public string content;

            public JsonBuildStepMessage(BuildStepMessage other)
            {
                type = other.type.ToString();
                content = other.content;
            }
        }
    }
}