using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aubergine;

namespace Game
{
    class Game : Space
    {

        private IInteraction<GameObject, GameObject>[] interactions;
        private ICollideInteraction<GameObject, GameObject>[] collideInteractions;


        /*
        class Health : IParameter<int> { }
        class Mana : IParameter<int> { }
        class NutritionalValue : IParameter<int> { }
        class Apple : IParametrizedGameObject { }
        class Hero : IParametrizedGameObject { }

        With<Pararmeter<T>>(T min, T max, T current) where T: IComparable
        */
        public Game()
        {
            #region
            /*
             var hero = Factory
                .GetParametrizedCharacter<Hero>()
                .OnPos(1, 2)
                .WithParameter<Health>(1, 100, 50)
                .WithParameter<Mana>(1, 100, 100)
                .AddInteraction<Apple>((her, appl) =>
                    {
                        her.Set<Health>(her.Get<Health> + appl.Get<Sytnost>);
                        appl.BeEaten();
                    })
                //.AddOnTickAction((her) => { her.Set<Health>(-1) });

            var wormFactory = MetaFactory
                .GetParametrizedCharacterFactory<Hero>()
                .WithParameter<Health>(1, 100, 50)
                .WithParameter<Mana>(1, 100)
                .AddInteraction<Apple>((her, appl) =>
                {
                    her.Set<Health>(her.Get < Health > +appl.Get<NutritionalValue>);
                    appl.BeEaten();
                })
                .AddOnTickAction((her) => { her.Set<Health>(-1) });
            var worm = wormFactory
                .OnPos(1, 2)
                .Set<Health>(50)
                .Set<Mana>(100)
                .Create();

            var apple = Factory
                .GetParametrizedCharacter<Apple>()
                .OnPos(1, 2)
                .WithParameter<Health>(1, 100, 50)
                .WithParameter<Mana>(1, 100, 100);

            c.Get<Health>();
            c.Set<Health>();

            */
            #endregion

            objects = new List<GameObject>
            {
                new Player(new Position() {Coords = new Point(450,200)}),
                new Worm(new Position() {Coords = new Point(100, 100)}),
                new Worm(new Position() {Coords = new Point(900, 100)}),
                new Worm(new Position() {Coords = new Point(100, 300)}),
                new Worm(new Position() {Coords = new Point(400, 500)})
            };

            var space = new Space(objects.ToArray());

        }


        public override bool Exist { get; }

    }
}
