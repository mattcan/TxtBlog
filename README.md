*This project is no longer maintained. It uses a preview version of .NET Core so if you'd like to work on this, I'd suggest starting a new project and taking the ideas. If you really want to take this project over, please reach out to [@mattcan](https://github.com/mattcan).*

# TxtBlog

A dynamic blog using Markdown files

## Features

* Yaml parsing through [YamlDotNet](https://www.nuget.org/packages/YamlDotNet/)
* Markdown parsing through [CommonMark.NET](https://www.nuget.org/packages/CommonMark.NET/)
* Show a list of posts with truncated output
* Show a single post
* Dockerfile for easy deployment

## Todo

* Link previous and next posts
* Caching of parsed posts
* Make the Tags useful
* Create an archive page with posts separated by month
* Review security

## Run it

If you're just interested in checking this out, add your posts to the `blog/` folder,
build the image, and then launch it.

    cd TxtBlog
    docker build -t txtblog .
    docker run -t -d -p 80:5001 txtblog

If you want to run it locally for development:

    cd TxtBlog
    dnu restore
    dnx --watch . kestrel
    
**Protip!** Press the `Enter` key to stop kestrel. `Ctrl+C` is not your friend here.
