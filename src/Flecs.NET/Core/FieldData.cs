namespace Flecs.NET.Core;

internal unsafe struct Fields
{
    public ecs_iter_t* Iter;
    public int Shared; // Is 0 indexed component.
    public int Sparse; // Is sparse component.
    public fixed ulong Pointers[16];

    public Fields(ecs_iter_t* iter, int count)
    {
        Iter = iter;

        for (byte i = 0; i < count; i++)
        {
            if (Utils.IsBitSet(iter->row_fields, i))
            {
                Sparse |= 1 << i;
            }
            else
            {
                Pointers[i] = (ulong)ecs_field_w_size(iter, iter->sizes[i], i);
                Shared |= iter->sources[i] == 0 && Utils.IsBitSet(iter->set_fields, i)
                    ? 0
                    : 1 << i;
            }
        }
    }
}
