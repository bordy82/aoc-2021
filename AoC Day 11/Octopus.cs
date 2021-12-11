namespace AoC_Day_11;

internal class Octopus
{
    public Octopus(int value)
    {
        this.Value = value;
        this.AdjacentOctopuses = new List<Octopus>();
    }

    public int Value { get; set; }

    public bool MustFlash { get; set; }

    public bool HasFlashed { get; set; }

    public List<Octopus> AdjacentOctopuses { get; private set; }

    internal void Tick()
    {
        this.Value += 1;
        if (this.Value > 9)
            this.MustFlash = true;
    }

    internal bool Flash()
    {
        if (this.MustFlash && !this.HasFlashed)
        {
            foreach(var octo in this.AdjacentOctopuses)
                octo.Tick();

            this.HasFlashed = true;
            this.MustFlash = false;

            return true;
        }

        return false;
    }

    internal void Reset()
    {
        if (this.HasFlashed)
            this.Value = 0;

        this.MustFlash = false;
        this.HasFlashed = false;
    }
}