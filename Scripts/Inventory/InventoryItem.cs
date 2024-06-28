[System.Serializable]
public class InventoryItem 
{
    public ProductType cropType;
    public int amount;

    public InventoryItem(ProductType cropType, int amount)
    {
        this.cropType = cropType;
        this.amount = amount;
    }
}
