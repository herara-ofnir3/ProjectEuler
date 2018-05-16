
lcm' x y
    | l == 1 || g == 1 = g
    | otherwise        = head [ m | m <- [g, g*2 ..], (mod m l) == 0 ]
    where
      g = maximum [x, y]
      l = minimum [x, y]


problem5 = foldl (lcm') 1 [1..20]

main = do
    print problem5
