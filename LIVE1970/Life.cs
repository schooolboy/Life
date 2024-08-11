using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIVE1970
{
	// перечисление возможных состояний клетки
	enum state_of_cell { ALIVE, DEAD, BORNED, DIED };

	class Life
    {
		const int size = 50;
		public space world;
        public Life(){
			world = new space();
        }

		// структура клетка
		public class cell
		{
			// по умолчанию клетка мертва
			public state_of_cell state;
			public cell() {
				state = state_of_cell.DEAD;
			}
		};

		// структура пространство
		public class space
		{
			public int size;
			public cell[,] universe = new cell[Life.size, Life.size];
			public int gen;
			public space() {
				size = Life.size;
				gen = 0;
				for (int i = 0; i < size; i++) {
					for (int j = 0; j < size; j++)
					{
						universe[i,j] = new cell();
					}
				}
			}
		};

		int trans(int i, int size)
		{
			if (i == -1)
			{
				return size - 1;
			}
			if (i == size)
			{
				return 0;
			}
			return i;
		}

		// функция обхода окружающих клеток (подсчет живых)
		void round(ref space world, int x, int y, ref int alives)
		{
			if (world.universe[trans(x - 1, world.size), y].state == state_of_cell.ALIVE || world.universe[trans(x - 1, world.size),y].state == state_of_cell.DIED)
			{
				alives++;
			}
			if (world.universe[trans(x - 1, world.size), trans(y - 1, world.size)].state ==state_of_cell.ALIVE || world.universe[trans(x - 1, world.size), trans(y - 1, world.size)].state ==  state_of_cell.DIED)
			{
				alives++;
			}
			if (world.universe[x, trans(y - 1, world.size)].state ==state_of_cell.ALIVE || world.universe[x, trans(y - 1, world.size)].state ==  state_of_cell.DIED)
			{
				alives++;
			}
			if (world.universe[trans(x + 1, world.size), trans(y - 1, world.size)].state == state_of_cell.ALIVE || world.universe[trans(x + 1, world.size), trans(y - 1, world.size)].state ==  state_of_cell.DIED)
			{
				alives++;
			}
			if (world.universe[trans(x + 1, world.size), y].state == state_of_cell.ALIVE || world.universe[trans(x + 1, world.size), y].state ==  state_of_cell.DIED)
			{
				alives++;
			}
			if (world.universe[trans(x + 1, world.size), trans(y + 1, world.size)].state == state_of_cell.ALIVE || world.universe[trans(x + 1, world.size), trans(y + 1, world.size)].state ==  state_of_cell.DIED)
			{
				alives++;
			}
			if (world.universe[x, trans(y + 1, world.size)].state == state_of_cell.ALIVE || world.universe[x, trans(y + 1, world.size)].state ==  state_of_cell.DIED)
			{
				alives++;
			}
			if (world.universe[trans(x - 1, world.size), trans(y + 1, world.size)].state == state_of_cell.ALIVE || world.universe[trans(x - 1, world.size), trans(y + 1, world.size)].state ==  state_of_cell.DIED)
			{
				alives++;
			}
		}

		// функция обрабатывающая клетку
		void processing(ref space world, int x, int y)
		{
			int alives = 0;
			round(ref world, x, y, ref alives);
			if (world.universe[x, y].state == state_of_cell.DEAD && alives == 3)
			{
				world.universe[x, y].state = state_of_cell.BORNED;
			}
			if (world.universe[x, y].state == state_of_cell.ALIVE && (alives < 2 || alives > 3))
			{
				world.universe[x, y].state =  state_of_cell.DIED;
			}
		}

		// функция пост-обработки клетки
		void post_processing(ref space world, int x, int y)
		{
			if (world.universe[x, y].state ==  state_of_cell.DIED)
			{
				world.universe[x, y].state = state_of_cell.DEAD;
			}
			if (world.universe[x, y].state == state_of_cell.BORNED)
			{
				world.universe[x, y].state = state_of_cell.ALIVE;
			}
		}

		public void clear_space()
		{
			int x, y;
			for (x = 0; x < world.size; x++)
			{
				for (y = 0; y < world.size; y++)
				{
					world.universe[x, y].state = state_of_cell.DEAD;
				}
			}
		}

		// просчитать 1 поколение
		public int Gen()
		{
			int x, y;
			for (x = 0; x < world.size; x++)
			{
				for (y = 0; y < world.size; y++)
				{
					processing(ref world, x, y);
				}
			}
			x = 0; y = 0;
			for (x = 0; x < world.size; x++)
			{
				for (y = 0; y < world.size; y++)
				{
					post_processing(ref world, x, y);
				}
			}
			world.gen++;
			return 1;
		}
	}
}
