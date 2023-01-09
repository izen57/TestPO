using Logic;

using Model;

using RepositoriesImplementations;

using System.Timers;


namespace E2ETests
{
	[TestClass]
	public class E2ENoteTest
	{
		INoteService _noteService;

		[TestInitialize]
		public void NoteCLI()
		{
			_noteService = new NoteService(new NoteFileRepo(), 0.000174); // 15 секунд
		}

		[TestMethod]
		public void Test()
		{
			Note ManualNote = new(Guid.NewGuid(), "Заметка без автоудаления", false);
			Note AutoNote = new(Guid.NewGuid(), "Заметка с автоудалением", true);
			_noteService.Create(ManualNote);
			_noteService.Create(AutoNote);

			while (_noteService.GetNote(AutoNote.Id) != null)
				;

			if (_noteService.GetNote(AutoNote.Id) != null || _noteService.GetNote(ManualNote.Id) == null)
				Assert.Fail();
		}
	}
}
