using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkoorCard.Services
{
	public interface IPlayerService
	{
		Task AddAsync(Player p);
		Task<IList<Player>> ListAsync();
		Task<Player> LoadAsync(Guid id);
	}

	public class PlayerService : IPlayerService
	{

		private List<Player> players;

		public PlayerService()
		{
			players = new List<Player>();
		}
		public async Task<IList<Player>> ListAsync()
		{
			return await Task.FromResult(players);
		}

		public async Task AddAsync(Player p)
		{
			await Task.Run(() =>
			{
				players.Add(p);
			});
		}

		public async Task<Player> LoadAsync(Guid id)
		{
			return await Task.FromResult(players.SingleOrDefault(x => x.Id.Equals(id)));
		}
	}
}