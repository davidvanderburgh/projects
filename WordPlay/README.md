Wordplay!

Part 1. WordSnake
Part 2. WordFind


Part 1: WordSnake

Here is a random grid of letters that is 10x10.  
Picking a starting location and continually moving to one adjacent square (including diagonals and potentially 
reusing the same letter twice in one word if you can get back to it), what's the longest word you can spell?  

For example, the following grid will produce one answer:  

--------------------------------------
e p d o b s l p o s  
o r q z z l r o b m  
p x c p a j a u i s  
b o g v i d l o x v  
d x t n f x m k s b  
d l s i o a e g h l  
l k z o k s d n j p  
s w j z c l w i d w  
p j u z h i p r a b  
r x z e d l q x v f  

The longest word found is:  
infinitizing  
--------------------------------------

The user can select the grid size between 5x5 and 20x20.  
In the following example, the user has chosen 15x15.  
Since there are multiple words found to be the longest, they are all displayed:  

--------------------------------------  
d a k c q a e e e n j w o v z  
p u o g r y e s c u a m t s m  
r m o o d p s n u e c n v y l  
g r h t q w c y n w r b a a j  
h e e k o p a t k d p z m u h  
d c m j g b x t v i h g u j e  
g k f d q r c d d o y q p i t  
p i w y m i h k m a d e b u z  
b y c y p x n b x j t r c n a  
d a d u m s c o q p r s l w f  
f t c w g t h k l o v z s i l  
j x q f p v t z b z r d e b q  
x u y k w t h y z a p j k y j  
l d z y y m v z z d f m u p h  
i v b n k s u b e k v r j x t  

The longest words found are:  
cinchoninic  
unsenescent  
torpescence  
thermomotor  
--------------------------------------  

The challenge in this example is to produce all answers in 5-10 seconds.  
The solution I designed demonstrates use of recursion.  



Part 2: WordFind

Using word entries from the user, create a word find grid with those words randomly placed.  
The words can be placed in any cardinal orientation and at any coordinate, as long as the
word does not fall off the grid.  
Words can share a space as long as the letters are identical.  

For example, the following words generate the grid below:  

-------------------------------------- 
write code enjoyably advance tech industry excite ideas creativity  

r r y e c n a v d a w  
y l c x e x r c s h o  
t z u c k t g f o y p  
i u q i e t i r w d q  
v v e t h c e t d c e  
i p t e i d e a s o h  
t q e n j o y a b l y  
a p a x f q k l r p g  
e y r t s u d n i h z  
r a h j r u m h a q t  
c b t t v k b s h x v  
-------------------------------------- 

The answers can be produced in an animation when the user is done:  

-------------------------------------- 
r r y e c n a v d a w  
y l c x e x r c s h o  
t z u c k t g f o y p  
i u q i e t i r w d q  
v v e t h c e t d c e  
i p t e i d e a s o h  
t q e n j o y a b l y  
a p a x f q k l r p g  
e y r t s u d n i h z  
r a h j r u m h a q t  
c b t t v k b s h x v  


      e c n a v d a  
y     x       c  
t     c         o  
i     i e t i r w d  
v     t h c e t     e  
i     e i d e a s  
t   e n j o y a b l y  
a  
e y r t s u d n i  
r  
c  

--------------------------------------   
