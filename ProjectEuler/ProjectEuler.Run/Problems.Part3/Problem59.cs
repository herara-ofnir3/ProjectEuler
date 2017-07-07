using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 59 「XOR暗号解読」
	/// (訳者注: 文字コードの説明は適当です) 各文字はそれぞれ一意のコードに割り当てられている. よく使われる標準としてASCII (American Standard Code for Information Interchange) がある. ASCIIでは, 大文字A = 65, アスタリスク (*) = 42, 小文字k = 107というふうに割り当てられている.
	/// 
	/// モダンな暗号化の方法として, テキストファイルの各バイトをASCIIに変換し, 秘密鍵から計算された値とXORを取るという手法がある. XOR関数の良い点は, 暗号化に用いたのと同じ暗号化鍵でXORを取ると平文を復号できる点である. 65 XOR 42 = 107であり, 107 XOR 42 = 65である.
	/// 
	/// 破られない暗号化のためには, 鍵は平文と同じ長さのランダムなバイト列でなければならない. ユーザーは暗号文と暗号化鍵を別々の場所に保存する必要がある. また, もし一方が失われると, 暗号文を復号することは不可能になる.
	/// 
	/// 悲しいかな, この手法はほとんどのユーザーにとって非現実的である. そこで, 鍵の変わりにパスワードを用いる手法が用いられる. パスワードが平文より短ければ (よくあることだが), パスワードは鍵として繰り返し用いられる. この手法では, 安全性を保つために十分長いパスワードを用いる必要があるが, 記憶するためにはある程度短くないといけない.
	/// 
	/// この問題での課題は簡単になっている. 暗号化鍵は3文字の小文字である. cipher1.txtは暗号化されたASCIIのコードを含んでいる. また, 平文はよく用いられる英単語を含んでいる. この暗号文を復号し, 平文のASCIIでの値の和を求めよ.
	/// </summary>
	public class Problem59 : Problem
	{
		public override string Run()
		{
			var raw = string.Empty;
			using (var fs = new FileStream("Assets\\Problem59\\cipher.txt", FileMode.Open))
				using (var reader = new StreamReader(fs))
					raw = reader.ReadToEnd();

			var ciphertexts = raw.Split(',')
				.Select(s => (char)int.Parse(s))
				.ToList();
			
			var candidates = Candidates(ciphertexts);
			// 候補を表示して人力でそれらしいものを探す...
			//foreach (var cand in candidates)
			//	Console.WriteLine($"{cand.Key}: {cand.Value.Substring(0, 50)}");

			var pass = ""; // ここにパスワードをいれると答えが出る。
			var answer = candidates[pass].ToCharArray().Sum(c => c);

			return answer.ToString();
		}

		static IDictionary<string, string> Candidates(IEnumerable<char> ciphertexts)
		{
			// 全てのパスワードのパターンを使って、全復号化パターンを列挙。
			var passchars = Enumerable.Range(97, 26).Select(x => (char)x).ToList();
			var patterns = Patterns(passchars, 3).Select(x => x.ToArray()).ToArray();
			var clearchars = Enumerable.Range(32, 95).Select(c => (char)c).ToList();

			// 普通の文字のみ含むパターンを候補として返す。
			var found = new Dictionary<string, string>();
			foreach (var pat in patterns)
			{
				var dec = Decrypt(ciphertexts, pat);
				if (dec.All(d => clearchars.Contains(d)))
				{
					found.Add(string.Join("", pat), string.Join("", dec));
				}
			}
			return found;
		}

		static IEnumerable<char> Decrypt(IEnumerable<char> chars, char[] pass)
		{
			var len = pass.Length;
			return chars.Select((c, i) => (char)(c ^ pass[i % len]));
		}

		static IEnumerable<IEnumerable<T>> Patterns<T>(IEnumerable<T> items, int? k = null)
		{
			if (k == null)
				k = items.Count();

			if (k == 0)
			{
				yield return Enumerable.Empty<T>();
			}
			else
			{
				var i = 0;
				foreach (var x in items)
				{
					foreach (var c in Patterns(items, k - 1))
						yield return c.Before(x);

					i++;
				}
			}
		}
	}
}
