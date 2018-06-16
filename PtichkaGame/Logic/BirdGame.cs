using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using Aubergine;
using Ninject;
using Ninject.Extensions.Conventions;
using System.Reflection;
using System;

namespace PtichkaGame.Logic
{
    class Game
    {
        private World world;
        private BirdPlayer player;
        public ImmutableList<GameObject> Objects => world.Objects;

        private void CollectGameByHands()
        {
            player = new BirdPlayer(new Position(new Point(450, 200), new Size(60, 50)));

            var objects = new GameObject[]
            {
                player,
                new Bush(new Position(new Point(100, 100), new Size(10, 10))),
                new Bush(new Position(new Point(900, 100), new Size(10, 10))),
                new Bush(new Position(new Point(100, 300), new Size(10, 10))),
                new Bush(new Position(new Point(400, 500), new Size(10, 10)))
            };

            world = new World(new FourWays(), objects);
            world.AddIConditionalEvent(new Collect());
        }

        private void CollectGameByDIContainer()
        {
            var container = new StandardKernel();

            container.Bind<Physics>().To<FourWays>();
            container.Bind<GameObject>().To<BirdPlayer>().InSingletonScope();
            container.Bind<GameObject>().To<Bush>();

            container.Bind<Position>()
                .ToConstant(new Position(new Point(100, 100), new Size(50, 40)))
                .WhenInjectedInto<BirdPlayer>();
            container.Bind<Position>()
                .ToConstant(new Position(new Point(10, 10), new Size(50, 50)))
                .WhenInjectedInto<Bush>();

            // Код, который требуется повторить для всех 
            // IConditionalEvent<T1, T2> where T1, T2 : GameObject:

            //  world.AddIConditionalEvent(new Collect());

            // Не работает из-за отсутствия ковариантности:
            /*container.Bind<IConditionalEvent<GameObject, GameObject>>().To<Collect>();
            container.Bind<World>().ToSelf()
                .OnActivation(w =>
                {
                    foreach (var c in container
                        .GetAll<IConditionalEvent<GameObject, GameObject>>())
                            w.AddIConditionalEvent(c);
                });
            */
            // Единственный способ повторить автоматически - но он ничего не сокращает
            
            container.Bind<IConditionalEvent<BirdPlayer, Bush>>().To<Collect>();
            container.Bind<World>().ToSelf()
                .OnActivation(w =>
                {
                    foreach (var c in container
                        .GetAll<IConditionalEvent<BirdPlayer, Bush>>())
                            w.AddIConditionalEvent(c);
                });

            world = container.Get<World>();
            player = container.Get<BirdPlayer>();

            var assembly = Assembly.GetExecutingAssembly();
            world.CollectConditionalEventsAutomatically(assembly);
        }

        public Game()
        {
            CollectGameByDIContainer();
        }

        public void Tick()
        {
            world.Tick();
        }

        public BirdPlayer GetPlayer()
        {
            return player;
        }

        public IEnumerable<GameObject> GetGameObjectsInRectangle(Rectangle rectangle)
        {
            return world.GetObjectsInRectangle(rectangle);
        }
    }

}