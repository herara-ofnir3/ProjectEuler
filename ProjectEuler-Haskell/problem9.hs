problem9 = head [ a * b * c | c <- [5..], 
                              b <- [4..(c - 1)], 
                              a <- [3..(b - 1)], 
                              (a^2 + b^2) == c^2,
                              a + b + c == 1000 ]

main = do
    print problem9
