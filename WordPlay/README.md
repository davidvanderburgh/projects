Wordplay!

Part 1. WordSnake

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






//write code enjoyably advance tech industry excite ideas