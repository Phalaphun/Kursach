namespace Kursach
{
    internal class BlockQueue
    {
        private Block[] blocks = new Block[]
        {
            new BlockI(),
            new BlockO(),
            new BlockJ(),
            new BlockL(),
            new BlockS(),
            new BlockT(),
            new BlockZ(),
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