using System;
using MediaArchieve.Shared;
using MediaArchieve.Shared.Items;

namespace MediaArchieve.Client.Model
{
    public class ItemFactory
    {
        public Item CreateItem(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Audio:
                    return new Audio("Новая аудиозапись", "Добавьте описание");
                case ItemType.Document:
                    return new Document("Новый документ", "Добавьте описание");
                case ItemType.Clip:
                    return new Clip("Новый клип", "Добавьте описание");
                case ItemType.Film:
                    return new Film("Новый фильм", "Добавьте описание");
                case ItemType.Photo:
                    return new Photo("Новая фотография", "Добавьте описание");
                default:
                    throw new ArgumentException();
            }
        }
    }
}