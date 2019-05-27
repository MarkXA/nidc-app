using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Xamarin.Forms;

namespace NidcApp.Controls
{
    public class MarkdownView : ContentView
    {
        public Action<string> NavigateToLink { get; set; } = s => Device.OpenUri(new Uri(s));

        public static MarkdownTheme Global = new LightMarkdownTheme();

        public string Markdown
        {
            get { return (string)GetValue(MarkdownProperty); }
            set { SetValue(MarkdownProperty, value); }
        }

        public static readonly BindableProperty MarkdownProperty = BindableProperty.Create(
            nameof(Markdown),
            typeof(string),
            typeof(MarkdownView),
            null,
            propertyChanged: OnMarkdownChanged);

        public string RelativeUrlHost
        {
            get { return (string)GetValue(RelativeUrlHostProperty); }
            set { SetValue(RelativeUrlHostProperty, value); }
        }

        public static readonly BindableProperty RelativeUrlHostProperty = BindableProperty.Create(
            nameof(RelativeUrlHost),
            typeof(string),
            typeof(MarkdownView),
            null,
            propertyChanged: OnMarkdownChanged);

        public MarkdownTheme Theme
        {
            get { return (MarkdownTheme)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        public static readonly BindableProperty ThemeProperty = BindableProperty.Create(
            nameof(Theme),
            typeof(MarkdownTheme),
            typeof(MarkdownView),
            Global,
            propertyChanged: OnMarkdownChanged);

        private bool isQuoted;

        private readonly List<View> queuedViews = new List<View>();

        static void OnMarkdownChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as MarkdownView;
            view.RenderMarkdown();
        }

        private StackLayout stack;

        private List<KeyValuePair<string, string>> links = new List<KeyValuePair<string, string>>();

        private void RenderMarkdown()
        {
            stack = new StackLayout {Spacing = Theme.Margin};

            Padding = Theme.Margin;

            BackgroundColor = Theme.BackgroundColor;

            if (!string.IsNullOrEmpty(Markdown))
            {
                var parsed = Markdig.Markdown.Parse(Markdown);
                Render(parsed.AsEnumerable());
            }

            Content = stack;
        }

        private void Render(IEnumerable<Block> blocks)
        {
            foreach (var block in blocks)
            {
                Render(block);
            }
        }

        private void AttachLinks(View view)
        {
            if (links.Any())
            {
                var blockLinks = links;
                view.GestureRecognizers.Add(
                    new TapGestureRecognizer
                    {
                        Command = new Command(
                            async () =>
                            {
                                try
                                {
                                    if (blockLinks.Count > 1)
                                    {
                                        var result = await Application.Current.MainPage.DisplayActionSheet(
                                            "Open link",
                                            "Cancel",
                                            null,
                                            blockLinks.Select(x => x.Key).ToArray());
                                        var link = blockLinks.FirstOrDefault(x => x.Key == result);
                                        NavigateToLink(link.Value);
                                    }
                                    else
                                    {
                                        NavigateToLink(blockLinks.First().Value);
                                    }
                                }
                                catch (Exception) { }
                            })
                    });

                links = new List<KeyValuePair<string, string>>();
            }
        }

        #region Rendering blocks

        private void Render(Block block)
        {
            switch (block)
            {
                case HeadingBlock heading:
                    Render(heading);
                    break;

                case ParagraphBlock paragraph:
                    Render(paragraph);
                    break;

                case QuoteBlock quote:
                    Render(quote);
                    break;

                case CodeBlock code:
                    Render(code);
                    break;

                case ListBlock list:
                    Render(list);
                    break;

                case ThematicBreakBlock thematicBreak:
                    Render(thematicBreak);
                    break;

                case HtmlBlock html:
                    Render(html);
                    break;

                default:
                    Debug.WriteLine($"Can't render {block.GetType()} blocks.");
                    break;
            }

            if (queuedViews.Any())
            {
                foreach (var view in queuedViews)
                {
                    stack.Children.Add(view);
                }

                queuedViews.Clear();
            }
        }

        private int listScope;

        private void Render(ThematicBreakBlock block)
        {
            var style = Theme.Separator;

            if (style.BorderSize > 0)
            {
                stack.Children.Add(new BoxView {HeightRequest = style.BorderSize, BackgroundColor = style.BorderColor});
            }
        }

        private void Render(ListBlock block)
        {
            listScope++;

            for (var i = 0; i < block.Count(); i++)
            {
                var item = block.ElementAt(i);

                if (item is ListItemBlock itemBlock)
                {
                    Render(block, i + 1, itemBlock);
                }
            }

            listScope--;
        }

        private void Render(ListBlock parent, int index, ListItemBlock block)
        {
            var initialStack = stack;

            stack = new StackLayout {Spacing = Theme.Margin};

            Render(block.AsEnumerable());

            var horizontalStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal, Margin = new Thickness(listScope * Theme.Margin, 0, 0, 0)
            };

            View bullet;

            if (parent.IsOrdered)
            {
                bullet = new Label
                {
                    Text = $"{index}.",
                    FontSize = Theme.Paragraph.FontSize,
                    TextColor = Theme.Paragraph.ForegroundColor,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.End
                };
            }
            else
            {
                bullet = new BoxView
                {
                    WidthRequest = 4,
                    HeightRequest = 4,
                    Margin = new Thickness(0, 6, 0, 0),
                    BackgroundColor = Theme.Paragraph.ForegroundColor,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Center
                };
            }

            horizontalStack.Children.Add(bullet);

            horizontalStack.Children.Add(stack);
            initialStack.Children.Add(horizontalStack);

            stack = initialStack;
        }

        private void Render(HeadingBlock block)
        {
            MarkdownStyle style;

            switch (block.Level)
            {
                case 1:
                    style = Theme.Heading1;
                    break;
                case 2:
                    style = Theme.Heading2;
                    break;
                case 3:
                    style = Theme.Heading3;
                    break;
                case 4:
                    style = Theme.Heading4;
                    break;
                case 5:
                    style = Theme.Heading5;
                    break;
                default:
                    style = Theme.Heading6;
                    break;
            }

            var foregroundColor = isQuoted ? Theme.Quote.ForegroundColor : style.ForegroundColor;

            var label = new Label
            {
                FormattedText = CreateFormatted(
                    block.Inline,
                    style.FontFamily,
                    style.Attributes,
                    foregroundColor,
                    style.BackgroundColor,
                    style.FontSize)
            };

            AttachLinks(label);

            if (style.BorderSize > 0)
            {
                var headingStack = new StackLayout();
                headingStack.Children.Add(label);
                headingStack.Children.Add(
                    new BoxView {HeightRequest = style.BorderSize, BackgroundColor = style.BorderColor});
                stack.Children.Add(headingStack);
            }
            else
            {
                stack.Children.Add(label);
            }
        }

        private void Render(ParagraphBlock block)
        {
            var style = Theme.Paragraph;
            var foregroundColor = isQuoted ? Theme.Quote.ForegroundColor : style.ForegroundColor;
            var label = new Label
            {
                FormattedText = CreateFormatted(
                    block.Inline,
                    style.FontFamily,
                    style.Attributes,
                    foregroundColor,
                    style.BackgroundColor,
                    style.FontSize)
            };
            AttachLinks(label);
            stack.Children.Add(label);
        }

        private void Render(HtmlBlock block)
        {
            // ?
        }

        private void Render(QuoteBlock block)
        {
            var initialIsQuoted = isQuoted;
            var initialStack = stack;

            isQuoted = true;
            stack = new StackLayout {Spacing = Theme.Margin};

            var style = Theme.Quote;

            if (style.BorderSize > 0)
            {
                var horizontalStack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal, BackgroundColor = Theme.Quote.BackgroundColor
                };

                horizontalStack.Children.Add(
                    new BoxView {WidthRequest = style.BorderSize, BackgroundColor = style.BorderColor});

                horizontalStack.Children.Add(stack);
                initialStack.Children.Add(horizontalStack);
            }
            else
            {
                stack.BackgroundColor = Theme.Quote.BackgroundColor;
                initialStack.Children.Add(stack);
            }

            Render(block.AsEnumerable());

            isQuoted = initialIsQuoted;
            stack = initialStack;
        }

        private void Render(CodeBlock block)
        {
            var style = Theme.Code;
            var label = new Label
            {
                TextColor = style.ForegroundColor,
                FontAttributes = style.Attributes,
                FontFamily = style.FontFamily,
                FontSize = style.FontSize,
                Text = string.Join(Environment.NewLine, block.Lines)
            };
            stack.Children.Add(
                new Frame
                {
                    CornerRadius = 3,
                    HasShadow = false,
                    Padding = Theme.Margin,
                    BackgroundColor = style.BackgroundColor,
                    Content = label
                });
        }

        private FormattedString CreateFormatted(
            ContainerInline inlines,
            string family,
            FontAttributes attributes,
            Color foregroundColor,
            Color backgroundColor,
            float size)
        {
            var fs = new FormattedString();

            foreach (var inline in inlines)
            {
                var spans = CreateSpans(inline, family, attributes, foregroundColor, backgroundColor, size);
                if (spans != null)
                {
                    foreach (var span in spans)
                    {
                        fs.Spans.Add(span);
                    }
                }
            }

            return fs;
        }

        private Span[] CreateSpans(
            Inline inline,
            string family,
            FontAttributes attributes,
            Color foregroundColor,
            Color backgroundColor,
            float size)
        {
            switch (inline)
            {
                case LiteralInline literal:
                    return new[]
                    {
                        new Span
                        {
                            Text =
                                literal.Content.Text.Substring(literal.Content.Start, literal.Content.Length),
                            FontAttributes = attributes,
                            ForegroundColor = foregroundColor,
                            BackgroundColor = backgroundColor,
                            FontSize = size,
                            FontFamily = family
                        }
                    };

                case EmphasisInline emphasis:
                    var childAttributes =
                        attributes | (emphasis.IsDouble ? FontAttributes.Bold : FontAttributes.Italic);
                    return emphasis.SelectMany(
                            x => CreateSpans(x, family, childAttributes, foregroundColor, backgroundColor, size))
                        .ToArray();

                case LineBreakInline breakline:
                    return new[] {new Span {Text = "\n"}};

                case LinkInline link:

                    var url = link.Url;

                    if (!(url.StartsWith("http://") || url.StartsWith("https://")))
                    {
                        url = $"{RelativeUrlHost?.TrimEnd('/')}/{url.TrimStart('/')}";
                    }

                    if (link.IsImage)
                    {
                        var image = new Image();

                        //if (Path.GetExtension(url) == ".svg")
                        //{
                        //    image.RenderSvg(url);
                        //}
                        //else
                        //{
                        image.Source = url;
                        //}

                        queuedViews.Add(image);
                        return new Span[0];
                    }
                    else
                    {
                        var spans = link.SelectMany(
                                x => CreateSpans(
                                    x,
                                    Theme.Link.FontFamily ?? family,
                                    Theme.Link.Attributes,
                                    Theme.Link.ForegroundColor,
                                    Theme.Link.BackgroundColor,
                                    size))
                            .ToArray();
                        links.Add(new KeyValuePair<string, string>(string.Join("", spans.Select(x => x.Text)), url));
                        return spans;
                    }

                case CodeInline code:
                    return new[]
                    {
                        new Span
                        {
                            Text = "\u2002",
                            FontSize = size,
                            FontFamily = Theme.Code.FontFamily,
                            ForegroundColor = Theme.Code.ForegroundColor,
                            BackgroundColor = Theme.Code.BackgroundColor
                        },
                        new Span
                        {
                            Text = code.Content,
                            FontAttributes = Theme.Code.Attributes,
                            FontSize = size,
                            FontFamily = Theme.Code.FontFamily,
                            ForegroundColor = Theme.Code.ForegroundColor,
                            BackgroundColor = Theme.Code.BackgroundColor
                        },
                        new Span
                        {
                            Text = "\u2002",
                            FontSize = size,
                            FontFamily = Theme.Code.FontFamily,
                            ForegroundColor = Theme.Code.ForegroundColor,
                            BackgroundColor = Theme.Code.BackgroundColor
                        }
                    };

                default:
                    Debug.WriteLine($"Can't render {inline.GetType()} inlines.");
                    return null;
            }
        }

        #endregion
    }

    public class MarkdownTheme
    {
        public MarkdownTheme()
        {
            Paragraph = new MarkdownStyle {Attributes = FontAttributes.None, FontSize = 12};

            Heading1 = new MarkdownStyle {Attributes = FontAttributes.Bold, BorderSize = 1, FontSize = 26};

            Heading2 = new MarkdownStyle {Attributes = FontAttributes.Bold, BorderSize = 1, FontSize = 22};

            Heading3 = new MarkdownStyle {Attributes = FontAttributes.Bold, FontSize = 20};

            Heading4 = new MarkdownStyle {Attributes = FontAttributes.Bold, FontSize = 18};

            Heading5 = new MarkdownStyle {Attributes = FontAttributes.Bold, FontSize = 16};

            Heading6 = new MarkdownStyle {Attributes = FontAttributes.Bold, FontSize = 14};

            Link = new MarkdownStyle {Attributes = FontAttributes.None, FontSize = 12};

            Code = new MarkdownStyle {Attributes = FontAttributes.None, FontSize = 12};

            Quote = new MarkdownStyle
            {
                Attributes = FontAttributes.None,
                BorderSize = 4,
                FontSize = 12,
                BackgroundColor = Color.Gray.MultiplyAlpha(.1)
            };

            Separator = new MarkdownStyle {BorderSize = 2};

            // Platform specific properties
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    Code.FontFamily = "Courier";
                    break;

                case Device.Android:
                    Code.FontFamily = "monospace";
                    break;
            }
        }

        public Color BackgroundColor { get; set; }

        public MarkdownStyle Paragraph { get; set; }

        public MarkdownStyle Heading1 { get; set; }

        public MarkdownStyle Heading2 { get; set; }

        public MarkdownStyle Heading3 { get; set; }

        public MarkdownStyle Heading4 { get; set; }

        public MarkdownStyle Heading5 { get; set; }

        public MarkdownStyle Heading6 { get; set; }

        public MarkdownStyle Quote { get; set; }

        public MarkdownStyle Separator { get; set; }

        public MarkdownStyle Link { get; set; }

        public MarkdownStyle Code { get; set; }

        public float Margin { get; set; } = 10;
    }

    public class LightMarkdownTheme : MarkdownTheme
    {
        public LightMarkdownTheme()
        {
            BackgroundColor = DefaultBackgroundColor;
            Paragraph.ForegroundColor = DefaultTextColor;
            Heading1.ForegroundColor = DefaultTextColor;
            Heading1.BorderColor = DefaultSeparatorColor;
            Heading2.ForegroundColor = DefaultTextColor;
            Heading2.BorderColor = DefaultSeparatorColor;
            Heading3.ForegroundColor = DefaultTextColor;
            Heading4.ForegroundColor = DefaultTextColor;
            Heading5.ForegroundColor = DefaultTextColor;
            Heading6.ForegroundColor = DefaultTextColor;
            Link.ForegroundColor = DefaultAccentColor;
            Code.ForegroundColor = DefaultTextColor;
            Code.BackgroundColor = DefaultCodeBackground;
            Quote.ForegroundColor = DefaultQuoteTextColor;
            Quote.BorderColor = DefaultQuoteBorderColor;
            Separator.BorderColor = DefaultSeparatorColor;
        }

        public static readonly Color DefaultBackgroundColor = Color.FromHex("#ffffff");

        public static readonly Color DefaultAccentColor = Color.FromHex("#0366d6");

        public static readonly Color DefaultTextColor = Color.FromHex("#24292e");

        public static readonly Color DefaultCodeBackground = Color.FromHex("#f6f8fa");

        public static readonly Color DefaultSeparatorColor = Color.FromHex("#eaecef");

        public static readonly Color DefaultQuoteTextColor = Color.FromHex("#6a737d");

        public static readonly Color DefaultQuoteBorderColor = Color.FromHex("#dfe2e5");
    }

    public class DarkMarkdownTheme : MarkdownTheme
    {
        public DarkMarkdownTheme()
        {
            BackgroundColor = DefaultBackgroundColor;
            Paragraph.ForegroundColor = DefaultTextColor;
            Heading1.ForegroundColor = DefaultTextColor;
            Heading1.BorderColor = DefaultSeparatorColor;
            Heading2.ForegroundColor = DefaultTextColor;
            Heading2.BorderColor = DefaultSeparatorColor;
            Heading3.ForegroundColor = DefaultTextColor;
            Heading4.ForegroundColor = DefaultTextColor;
            Heading5.ForegroundColor = DefaultTextColor;
            Heading6.ForegroundColor = DefaultTextColor;
            Link.ForegroundColor = DefaultAccentColor;
            Code.ForegroundColor = DefaultTextColor;
            Code.BackgroundColor = DefaultCodeBackground;
            Quote.ForegroundColor = DefaultQuoteTextColor;
            Quote.BorderColor = DefaultQuoteBorderColor;
            Separator.BorderColor = DefaultSeparatorColor;
        }

        public static readonly Color DefaultBackgroundColor = Color.FromHex("#2b303b");

        public static readonly Color DefaultAccentColor = Color.FromHex("#d08770");

        public static readonly Color DefaultTextColor = Color.FromHex("#eff1f5");

        public static readonly Color DefaultCodeBackground = Color.FromHex("#4f5b66");

        public static readonly Color DefaultSeparatorColor = Color.FromHex("#65737e");

        public static readonly Color DefaultQuoteTextColor = Color.FromHex("#a7adba");

        public static readonly Color DefaultQuoteBorderColor = Color.FromHex("#a7adba");
    }

    public class MarkdownStyle
    {
        public FontAttributes Attributes { get; set; } = FontAttributes.None;

        public float FontSize { get; set; } = 12;

        public Color ForegroundColor { get; set; } = Color.Black;

        public Color BackgroundColor { get; set; } = Color.Transparent;

        public Color BorderColor { get; set; }

        public float BorderSize { get; set; }

        public string FontFamily { get; set; }
    }
}