using Model;

using Repositories;

using Serilog;

using System.Timers;

namespace Logic
{
	public class NoteService: INoteService
	{
		INoteRepo _repository;
		System.Timers.Timer _checkForTime;

		public NoteService(INoteRepo repo, double NoteDeleteTime)
		{
			Log.Logger = new LoggerConfiguration()
				.WriteTo.File("LogNote.txt")
				.CreateLogger();

			_repository = repo ?? throw new ArgumentNullException(nameof(repo));
			
			_checkForTime = new(6 * 1000);
			_checkForTime.Elapsed += (sender, e) => AutoDelete(sender, e, NoteDeleteTime);
			_checkForTime.Enabled = true;
		}

		public void Create(Note note)
		{
			_repository.Create(note);
		}

		public void Edit(Note note)
		{
			_repository.Edit(note);
		}

		public void Delete(Guid id)
		{
			_repository.Delete(id);
		}

		public List<Note> GetAllNotesList()
		{
			return _repository.GetAllNotesList();
		}

		public Note? GetNote(Guid guid)
		{
			return _repository.GetNote(guid);
		}

		private void AutoDelete(object sender, ElapsedEventArgs e, double fromDays)
		{
			foreach (Note note in GetAllNotesList())
				if (note.IsTemporal == true && DateTime.Now - note.CreationTime >= TimeSpan.FromDays(fromDays))
				{
					Log.Logger.Information($"Заметка удалена автоматически по истечении срока. Идентификатор заметки: {note.Id}.");
					_repository.Delete(note.Id);
				}
		}
	}
}
