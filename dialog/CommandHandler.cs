using Godot;
using System;
using System.Linq;
using YarnSpinnerGodot;

public partial class CommandHandler : Node
{
	private static readonly string[] CommandNames =
	[
		"slot",
		"pose",
		"turn",
		"animate_slot",
		"move_slot",
	];

	public override void _Ready()
	{
		var dialogueRunner = GetParent()?.FindChild("DialogueRunner", true, false) as DialogueRunner
			?? GetTree().Root.FindChild("DialogueRunner", true, false) as DialogueRunner;

		if (dialogueRunner == null)
		{
			GD.PushError($"{nameof(CommandHandler)} could not find a DialogueRunner.");
			return;
		}

		foreach (var commandName in CommandNames)
		{
			dialogueRunner.AddCommandHandler(commandName, (Action<string[]>)(args => LogCommand(commandName, args)));
		}
	}

	private static void LogCommand(string commandName, string[] args)
	{
		var arguments = args.Length == 0
			? string.Empty
			: $" {string.Join(" ", args.Select(arg => $"\"{arg}\""))}";

		GD.Print($"Yarn command: {commandName}{arguments}");
	}
}
