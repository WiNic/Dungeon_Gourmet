using UnrealBuildTool;

public class DungeonGourmetTarget : TargetRules
{
	public DungeonGourmetTarget(TargetInfo Target) : base(Target)
	{
		DefaultBuildSettings = BuildSettingsVersion.V2;
		Type = TargetType.Game;
		ExtraModuleNames.Add("DungeonGourmet");
	}
}
