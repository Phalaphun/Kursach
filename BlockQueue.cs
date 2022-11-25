﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach
{
    internal class BlockQueue
    {
        private Block[] blocks = new Block[]
        {
            new BlockI(),
            new BlockO(),
        };

        private Random random = new Random(); 
        public Block NextBlock { get; set; }

        private Block RandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        public BlockQueue()
        {
            NextBlock = RandomBlock();
        }

        public Block GetAndUpdate()
        {
            Block block = NextBlock;
            do
            {
                NextBlock = RandomBlock();
            } while (block.Id == NextBlock.Id);

            return block;

        }
    }
}
