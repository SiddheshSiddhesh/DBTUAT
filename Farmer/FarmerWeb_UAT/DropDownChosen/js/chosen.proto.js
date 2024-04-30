/* Chosen v0.12.1 | (c) 2011-2013 by Harvest | MIT License, https://github.com/harvesthq/chosen/blob/master/LICENSE.md */
!function(){var SelectParser;SelectParser=function(){function SelectParser(){this.options_index=0,this.parsed=[]}return SelectParser.prototype.add_node=function(a){return"OPTGROUP"===a.nodeName.toUpperCase()?this.add_group(a):this.add_option(a)},SelectParser.prototype.add_group=function(a){var b,c,d,e,f,g;for(b=this.parsed.length,this.parsed.push({array_index:b,group:!0,label:a.label,children:0,disabled:a.disabled}),f=a.childNodes,g=[],d=0,e=f.length;e>d;d++)c=f[d],g.push(this.add_option(c,b,a.disabled));return g},SelectParser.prototype.add_option=function(a,b,c){return"OPTION"===a.nodeName.toUpperCase()?(""!==a.text?(null!=b&&(this.parsed[b].children+=1),this.parsed.push({array_index:this.parsed.length,options_index:this.options_index,value:a.value,text:a.text,html:a.innerHTML,selected:a.selected,disabled:c===!0?c:a.disabled,group_array_index:b,classes:a.className,style:a.style.cssText})):this.parsed.push({array_index:this.parsed.length,options_index:this.options_index,empty:!0}),this.options_index+=1):void 0},SelectParser}(),SelectParser.select_to_array=function(a){var b,c,d,e,f;for(c=new SelectParser,f=a.childNodes,d=0,e=f.length;e>d;d++)b=f[d],c.add_node(b);return c.parsed},this.SelectParser=SelectParser}.call(this),function(){var AbstractChosen,a;a=this,AbstractChosen=function(){function AbstractChosen(a,b){this.form_field=a,this.options=null!=b?b:{},AbstractChosen.browser_is_supported()&&(this.is_multiple=this.form_field.multiple,this.set_default_text(),this.set_default_values(),this.setup(),this.set_up_html(),this.register_observers(),this.finish_setup())}return AbstractChosen.prototype.set_default_values=function(){var a=this;return this.click_test_action=function(b){return a.test_active_click(b)},this.activate_action=function(b){return a.activate_field(b)},this.active_field=!1,this.mouse_on_container=!1,this.results_showing=!1,this.result_highlighted=null,this.result_single_selected=null,this.allow_single_deselect=null!=this.options.allow_single_deselect&&null!=this.form_field.options[0]&&""===this.form_field.options[0].text?this.options.allow_single_deselect:!1,this.disable_search_threshold=this.options.disable_search_threshold||0,this.disable_search=this.options.disable_search||!1,this.enable_split_word_search=null!=this.options.enable_split_word_search?this.options.enable_split_word_search:!0,this.search_contains=this.options.search_contains||!1,this.single_backstroke_delete=this.options.single_backstroke_delete||!1,this.max_selected_options=this.options.max_selected_options||1/0,this.inherit_select_classes=this.options.inherit_select_classes||!1},AbstractChosen.prototype.set_default_text=function(){return this.default_text=this.form_field.getAttribute("data-placeholder")?this.form_field.getAttribute("data-placeholder"):this.is_multiple?this.options.placeholder_text_multiple||this.options.placeholder_text||AbstractChosen.default_multiple_text:this.options.placeholder_text_single||this.options.placeholder_text||AbstractChosen.default_single_text,this.results_none_found=this.form_field.getAttribute("data-no_results_text")||this.options.no_results_text||AbstractChosen.default_no_result_text},AbstractChosen.prototype.mouse_enter=function(){return this.mouse_on_container=!0},AbstractChosen.prototype.mouse_leave=function(){return this.mouse_on_container=!1},AbstractChosen.prototype.input_focus=function(){var a=this;if(this.is_multiple){if(!this.active_field)return setTimeout(function(){return a.container_mousedown()},50)}else if(!this.active_field)return this.activate_field()},AbstractChosen.prototype.input_blur=function(){var a=this;return this.mouse_on_container?void 0:(this.active_field=!1,setTimeout(function(){return a.blur_test()},100))},AbstractChosen.prototype.results_option_build=function(a){var b,c,d,e,f;for(b="",f=this.results_data,d=0,e=f.length;e>d;d++)c=f[d],c.group&&c.search_match?b+=this.result_add_group(c):!c.empty&&c.search_match&&(b+=this.result_add_option(c)),(null!=a?a.first:void 0)&&(c.selected&&this.is_multiple?this.choice_build(c):c.selected&&!this.is_multiple&&this.single_set_selected_text(c.text));return b},AbstractChosen.prototype.result_add_option=function(a){var b,c;return a.dom_id=this.container_id+"_o_"+a.array_index,b=[],a.disabled||a.selected&&this.is_multiple||b.push("active-result"),!a.disabled||a.selected&&this.is_multiple||b.push("disabled-result"),a.selected&&b.push("result-selected"),null!=a.group_array_index&&b.push("group-option"),""!==a.classes&&b.push(a.classes),c=""!==a.style.cssText?' style="'+a.style+'"':"",'<li id="'+a.dom_id+'" class="'+b.join(" ")+'"'+c+">"+a.search_text+"</li>"},AbstractChosen.prototype.results_update_field=function(){return this.set_default_text(),this.is_multiple||this.results_reset_cleanup(),this.result_clear_highlight(),this.result_single_selected=null,this.results_build()},AbstractChosen.prototype.results_toggle=function(){return this.results_showing?this.results_hide():this.results_show()},AbstractChosen.prototype.results_search=function(){return this.results_showing?this.winnow_results():this.results_show()},AbstractChosen.prototype.winnow_results=function(){var a,b,c,d,e,f,g,h,i,j,k,l,m,n,o;for(this.no_results_clear(),f=0,g=this.get_search_text(),e=this.search_contains?"":"^",d=new RegExp(e+g.replace(/[-[\]{}()*+?.,\\^$|#\s]/g,"\\$&"),"i"),j=new RegExp(g.replace(/[-[\]{}()*+?.,\\^$|#\s]/g,"\\$&"),"i"),o=this.results_data,k=0,m=o.length;m>k;k++)if(a=o[k],!a.empty)if(a.group)a.search_match=!1;else{if(a.search_match=!1,d.test(a.html))a.search_match=!0,f+=1;else if(this.enable_split_word_search&&(a.html.indexOf(" ")>=0||0===a.html.indexOf("["))&&(c=a.html.replace(/\[|\]/g,"").split(" "),c.length))for(l=0,n=c.length;n>l;l++)b=c[l],d.test(b)&&(a.search_match=!0,f+=1);a.search_match&&(g.length?(h=a.html.search(j),i=a.html.substr(0,h+g.length)+"</em>"+a.html.substr(h+g.length),i=i.substr(0,h)+"<em>"+i.substr(h)):i=a.html,a.search_text=i,null!=a.group_array_index&&(this.results_data[a.group_array_index].search_match=!0))}return 1>f&&g.length?(this.update_results_content(""),this.no_results(g)):(this.update_results_content(this.results_option_build()),this.winnow_results_set_highlight())},AbstractChosen.prototype.choices_count=function(){var a,b,c,d;if(null!=this.selected_option_count)return this.selected_option_count;for(this.selected_option_count=0,d=this.form_field.options,b=0,c=d.length;c>b;b++)a=d[b],a.selected&&(this.selected_option_count+=1);return this.selected_option_count},AbstractChosen.prototype.choices_click=function(a){return a.preventDefault(),this.results_showing||this.is_disabled?void 0:this.results_show()},AbstractChosen.prototype.keyup_checker=function(a){var b,c;switch(b=null!=(c=a.which)?c:a.keyCode,this.search_field_scale(),b){case 8:if(this.is_multiple&&this.backstroke_length<1&&this.choices_count()>0)return this.keydown_backstroke();if(!this.pending_backstroke)return this.result_clear_highlight(),this.results_search();break;case 13:if(a.preventDefault(),this.results_showing)return this.result_select(a);break;case 27:return this.results_showing&&this.results_hide(),!0;case 9:case 38:case 40:case 16:case 91:case 17:break;default:return this.results_search()}},AbstractChosen.prototype.generate_field_id=function(){var a;return a=this.generate_random_id(),this.form_field.id=a,a},AbstractChosen.prototype.generate_random_char=function(){var a,b,c;return a="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",c=Math.floor(Math.random()*a.length),b=a.substring(c,c+1)},AbstractChosen.prototype.container_width=function(){return null!=this.options.width?this.options.width:""+this.form_field.offsetWidth+"px"},AbstractChosen.browser_is_supported=function(){var a;return"Microsoft Internet Explorer"===window.navigator.appName?null!==(a=document.documentMode)&&a>=8:!0},AbstractChosen.default_multiple_text="Select Some Options",AbstractChosen.default_single_text="Select an Option",AbstractChosen.default_no_result_text="No results match",AbstractChosen}(),a.AbstractChosen=AbstractChosen}.call(this),function(){var Chosen,a,b,c={}.hasOwnProperty,d=function(a,b){function d(){this.constructor=a}for(var e in b)c.call(b,e)&&(a[e]=b[e]);return d.prototype=b.prototype,a.prototype=new d,a.__super__=b.prototype,a};a=this,Chosen=function(c){function Chosen(){return b=Chosen.__super__.constructor.apply(this,arguments)}return d(Chosen,c),Chosen.prototype.setup=function(){return this.current_selectedIndex=this.form_field.selectedIndex,this.is_rtl=this.form_field.hasClassName("chzn-rtl")},Chosen.prototype.finish_setup=function(){return this.form_field.addClassName("chzn-done")},Chosen.prototype.set_default_values=function(){return Chosen.__super__.set_default_values.call(this),this.single_temp=new Template('<a href="javascript:void(0)" class="chzn-single chzn-default" tabindex="-1"><span>#{default}</span><div><b></b></div></a><div class="chzn-drop"><div class="chzn-search"><input type="text" autocomplete="off" /></div><ul class="chzn-results"></ul></div>'),this.multi_temp=new Template('<ul class="chzn-choices"><li class="search-field"><input type="text" value="#{default}" class="default" autocomplete="off" style="width:25px;" /></li></ul><div class="chzn-drop"><ul class="chzn-results"></ul></div>'),this.no_results_temp=new Template('<li class="no-results">'+this.results_none_found+' "<span>#{terms}</span>"</li>')},Chosen.prototype.set_up_html=function(){var a,b;return this.container_id=this.form_field.identify().replace(/[^\w]/g,"_")+"_chzn",a=["chzn-container"],a.push("chzn-container-"+(this.is_multiple?"multi":"single")),this.inherit_select_classes&&this.form_field.className&&a.push(this.form_field.className),this.is_rtl&&a.push("chzn-rtl"),b={id:this.container_id,"class":a.join(" "),style:"width: "+this.container_width()+";",title:this.form_field.title},this.container=this.is_multiple?new Element("div",b).update(this.multi_temp.evaluate({"default":this.default_text})):new Element("div",b).update(this.single_temp.evaluate({"default":this.default_text})),this.form_field.hide().insert({after:this.container}),this.dropdown=this.container.down("div.chzn-drop"),this.search_field=this.container.down("input"),this.search_results=this.container.down("ul.chzn-results"),this.search_field_scale(),this.search_no_results=this.container.down("li.no-results"),this.is_multiple?(this.search_choices=this.container.down("ul.chzn-choices"),this.search_container=this.container.down("li.search-field")):(this.search_container=this.container.down("div.chzn-search"),this.selected_item=this.container.down(".chzn-single")),this.results_build(),this.set_tab_index(),this.set_label_behavior(),this.form_field.fire("liszt:ready",{chosen:this})},Chosen.prototype.register_observers=function(){var a=this;return this.container.observe("mousedown",function(b){return a.container_mousedown(b)}),this.container.observe("mouseup",function(b){return a.container_mouseup(b)}),this.container.observe("mouseenter",function(b){return a.mouse_enter(b)}),this.container.observe("mouseleave",function(b){return a.mouse_leave(b)}),this.search_results.observe("mouseup",function(b){return a.search_results_mouseup(b)}),this.search_results.observe("mouseover",function(b){return a.search_results_mouseover(b)}),this.search_results.observe("mouseout",function(b){return a.search_results_mouseout(b)}),this.search_results.observe("mousewheel",function(b){return a.search_results_mousewheel(b)}),this.search_results.observe("DOMMouseScroll",function(b){return a.search_results_mousewheel(b)}),this.form_field.observe("liszt:updated",function(b){return a.results_update_field(b)}),this.form_field.observe("liszt:activate",function(b){return a.activate_field(b)}),this.form_field.observe("liszt:open",function(b){return a.container_mousedown(b)}),this.search_field.observe("blur",function(b){return a.input_blur(b)}),this.search_field.observe("keyup",function(b){return a.keyup_checker(b)}),this.search_field.observe("keydown",function(b){return a.keydown_checker(b)}),this.search_field.observe("focus",function(b){return a.input_focus(b)}),this.is_multiple?this.search_choices.observe("click",function(b){return a.choices_click(b)}):this.container.observe("click",function(a){return a.preventDefault()})},Chosen.prototype.search_field_disabled=function(){return this.is_disabled=this.form_field.disabled,this.is_disabled?(this.container.addClassName("chzn-disabled"),this.search_field.disabled=!0,this.is_multiple||this.selected_item.stopObserving("focus",this.activate_action),this.close_field()):(this.container.removeClassName("chzn-disabled"),this.search_field.disabled=!1,this.is_multiple?void 0:this.selected_item.observe("focus",this.activate_action))},Chosen.prototype.container_mousedown=function(a){return this.is_disabled||(a&&"mousedown"===a.type&&!this.results_showing&&a.stop(),null!=a&&a.target.hasClassName("search-choice-close"))?void 0:(this.active_field?this.is_multiple||!a||a.target!==this.selected_item&&!a.target.up("a.chzn-single")||this.results_toggle():(this.is_multiple&&this.search_field.clear(),document.observe("click",this.click_test_action),this.results_show()),this.activate_field())},Chosen.prototype.container_mouseup=function(a){return"ABBR"!==a.target.nodeName||this.is_disabled?void 0:this.results_reset(a)},Chosen.prototype.search_results_mousewheel=function(a){var b;return b=-a.wheelDelta||a.detail,null!=b?(a.preventDefault(),"DOMMouseScroll"===a.type&&(b=40*b),this.search_results.scrollTop=b+this.search_results.scrollTop):void 0},Chosen.prototype.blur_test=function(){return!this.active_field&&this.container.hasClassName("chzn-container-active")?this.close_field():void 0},Chosen.prototype.close_field=function(){return document.stopObserving("click",this.click_test_action),this.active_field=!1,this.results_hide(),this.container.removeClassName("chzn-container-active"),this.clear_backstroke(),this.show_search_field_default(),this.search_field_scale()},Chosen.prototype.activate_field=function(){return this.container.addClassName("chzn-container-active"),this.active_field=!0,this.search_field.value=this.search_field.value,this.search_field.focus()},Chosen.prototype.test_active_click=function(a){return a.target.up("#"+this.container_id)?this.active_field=!0:this.close_field()},Chosen.prototype.results_build=function(){return this.parsing=!0,this.selected_option_count=null,this.results_data=a.SelectParser.select_to_array(this.form_field),this.is_multiple?this.search_choices.select("li.search-choice").invoke("remove"):this.is_multiple||(this.single_set_selected_text(),this.disable_search||this.form_field.options.length<=this.disable_search_threshold?(this.search_field.readOnly=!0,this.container.addClassName("chzn-container-single-nosearch")):(this.search_field.readOnly=!1,this.container.removeClassName("chzn-container-single-nosearch"))),this.update_results_content(this.results_option_build({first:!0})),this.search_field_disabled(),this.show_search_field_default(),this.search_field_scale(),this.parsing=!1},Chosen.prototype.result_add_group=function(a){return a.dom_id=this.container_id+"_g_"+a.array_index,'<li id="'+a.dom_id+'" class="group-result">'+a.label.escapeHTML()+"</li>"},Chosen.prototype.result_do_highlight=function(a){var b,c,d,e,f;return this.result_clear_highlight(),this.result_highlight=a,this.result_highlight.addClassName("highlighted"),d=parseInt(this.search_results.getStyle("maxHeight"),10),f=this.search_results.scrollTop,e=d+f,c=this.result_highlight.positionedOffset().top,b=c+this.result_highlight.getHeight(),b>=e?this.search_results.scrollTop=b-d>0?b-d:0:f>c?this.search_results.scrollTop=c:void 0},Chosen.prototype.result_clear_highlight=function(){return this.result_highlight&&this.result_highlight.removeClassName("highlighted"),this.result_highlight=null},Chosen.prototype.results_show=function(){return this.is_multiple&&this.max_selected_options<=this.choices_count()?(this.form_field.fire("liszt:maxselected",{chosen:this}),!1):(this.container.addClassName("chzn-with-drop"),this.form_field.fire("liszt:showing_dropdown",{chosen:this}),this.results_showing=!0,this.search_field.focus(),this.search_field.value=this.search_field.value,this.winnow_results())},Chosen.prototype.update_results_content=function(a){return this.search_results.update(a)},Chosen.prototype.results_hide=function(){return this.results_showing&&(this.result_clear_highlight(),this.container.removeClassName("chzn-with-drop"),this.form_field.fire("liszt:hiding_dropdown",{chosen:this})),this.results_showing=!1},Chosen.prototype.set_tab_index=function(){var a;return this.form_field.tabIndex?(a=this.form_field.tabIndex,this.form_field.tabIndex=-1,this.search_field.tabIndex=a):void 0},Chosen.prototype.set_label_behavior=function(){var a=this;return this.form_field_label=this.form_field.up("label"),null==this.form_field_label&&(this.form_field_label=$$("label[for='"+this.form_field.id+"']").first()),null!=this.form_field_label?this.form_field_label.observe("click",function(b){return a.is_multiple?a.container_mousedown(b):a.activate_field()}):void 0},Chosen.prototype.show_search_field_default=function(){return this.is_multiple&&this.choices_count()<1&&!this.active_field?(this.search_field.value=this.default_text,this.search_field.addClassName("default")):(this.search_field.value="",this.search_field.removeClassName("default"))},Chosen.prototype.search_results_mouseup=function(a){var b;return b=a.target.hasClassName("active-result")?a.target:a.target.up(".active-result"),b?(this.result_highlight=b,this.result_select(a),this.search_field.focus()):void 0},Chosen.prototype.search_results_mouseover=function(a){var b;return b=a.target.hasClassName("active-result")?a.target:a.target.up(".active-result"),b?this.result_do_highlight(b):void 0},Chosen.prototype.search_results_mouseout=function(a){return a.target.hasClassName("active-result")||a.target.up(".active-result")?this.result_clear_highlight():void 0},Chosen.prototype.choice_build=function(a){var b,c,d=this;return b=new Element("li",{"class":"search-choice"}).update("<span>"+a.html+"</span>"),a.disabled?b.addClassName("search-choice-disabled"):(c=new Element("a",{href:"#","class":"search-choice-close",rel:a.array_index}),c.observe("click",function(a){return d.choice_destroy_link_click(a)}),b.insert(c)),this.search_container.insert({before:b})},Chosen.prototype.choice_destroy_link_click=function(a){return a.preventDefault(),a.stopPropagation(),this.is_disabled?void 0:this.choice_destroy(a.target)},Chosen.prototype.choice_destroy=function(a){return this.result_deselect(a.readAttribute("rel"))?(this.show_search_field_default(),this.is_multiple&&this.choices_count()>0&&this.search_field.value.length<1&&this.results_hide(),a.up("li").remove(),this.search_field_scale()):void 0},Chosen.prototype.results_reset=function(){return this.form_field.options[0].selected=!0,this.selected_option_count=null,this.single_set_selected_text(),this.show_search_field_default(),this.results_reset_cleanup(),"function"==typeof Event.simulate&&this.form_field.simulate("change"),this.active_field?this.results_hide():void 0},Chosen.prototype.results_reset_cleanup=function(){var a;return this.current_selectedIndex=this.form_field.selectedIndex,a=this.selected_item.down("abbr"),a?a.remove():void 0},Chosen.prototype.result_select=function(a){var b,c,d;return this.result_highlight?(b=this.result_highlight,this.result_clear_highlight(),this.is_multiple&&this.max_selected_options<=this.choices_count()?(this.form_field.fire("liszt:maxselected",{chosen:this}),!1):(this.is_multiple?b.removeClassName("active-result"):(this.search_results.descendants(".result-selected").invoke("removeClassName","result-selected"),this.result_single_selected=b),b.addClassName("result-selected"),d=b.id.substr(b.id.lastIndexOf("_")+1),c=this.results_data[d],c.selected=!0,this.form_field.options[c.options_index].selected=!0,this.selected_option_count=null,this.is_multiple?this.choice_build(c):this.single_set_selected_text(c.text),(a.metaKey||a.ctrlKey)&&this.is_multiple||this.results_hide(),this.search_field.value="","function"!=typeof Event.simulate||!this.is_multiple&&this.form_field.selectedIndex===this.current_selectedIndex||this.form_field.simulate("change"),this.current_selectedIndex=this.form_field.selectedIndex,this.search_field_scale())):void 0},Chosen.prototype.single_set_selected_text=function(a){return null==a&&(a=this.default_text),a===this.default_text?this.selected_item.addClassName("chzn-default"):(this.single_deselect_control_build(),this.selected_item.removeClassName("chzn-default")),this.selected_item.down("span").update(a)},Chosen.prototype.result_deselect=function(a){var b,c;return c=this.results_data[a],this.form_field.options[c.options_index].disabled?!1:(c.selected=!1,this.form_field.options[c.options_index].selected=!1,this.selected_option_count=null,b=$(this.container_id+"_o_"+a),b.removeClassName("result-selected").addClassName("active-result").show(),this.result_clear_highlight(),this.results_showing&&this.winnow_results(),"function"==typeof Event.simulate&&this.form_field.simulate("change"),this.search_field_scale(),!0)},Chosen.prototype.single_deselect_control_build=function(){return this.allow_single_deselect?(this.selected_item.down("abbr")||this.selected_item.down("span").insert({after:'<abbr class="search-choice-close"></abbr>'}),this.selected_item.addClassName("chzn-single-with-deselect")):void 0},Chosen.prototype.get_search_text=function(){return this.search_field.value===this.default_text?"":this.search_field.value.strip().escapeHTML()},Chosen.prototype.winnow_results_set_highlight=function(){var a;return this.is_multiple||(a=this.search_results.down(".result-selected.active-result")),null==a&&(a=this.search_results.down(".active-result")),null!=a?this.result_do_highlight(a):void 0},Chosen.prototype.no_results=function(a){return this.search_results.insert(this.no_results_temp.evaluate({terms:a}))},Chosen.prototype.no_results_clear=function(){var a,b;for(a=null,b=[];a=this.search_results.down(".no-results");)b.push(a.remove());return b},Chosen.prototype.keydown_arrow=function(){var a;return this.results_showing&&this.result_highlight?(a=this.result_highlight.next(".active-result"))?this.result_do_highlight(a):void 0:this.results_show()},Chosen.prototype.keyup_arrow=function(){var a,b,c;return this.results_showing||this.is_multiple?this.result_highlight?(c=this.result_highlight.previousSiblings(),a=this.search_results.select("li.active-result"),b=c.intersect(a),b.length?this.result_do_highlight(b.first()):(this.choices_count()>0&&this.results_hide(),this.result_clear_highlight())):void 0:this.results_show()},Chosen.prototype.keydown_backstroke=function(){var a;return this.pending_backstroke?(this.choice_destroy(this.pending_backstroke.down("a")),this.clear_backstroke()):(a=this.search_container.siblings().last(),a&&a.hasClassName("search-choice")&&!a.hasClassName("search-choice-disabled")?(this.pending_backstroke=a,this.pending_backstroke&&this.pending_backstroke.addClassName("search-choice-focus"),this.single_backstroke_delete?this.keydown_backstroke():this.pending_backstroke.addClassName("search-choice-focus")):void 0)},Chosen.prototype.clear_backstroke=function(){return this.pending_backstroke&&this.pending_backstroke.removeClassName("search-choice-focus"),this.pending_backstroke=null},Chosen.prototype.keydown_checker=function(a){var b,c;switch(b=null!=(c=a.which)?c:a.keyCode,this.search_field_scale(),8!==b&&this.pending_backstroke&&this.clear_backstroke(),b){case 8:this.backstroke_length=this.search_field.value.length;break;case 9:this.results_showing&&!this.is_multiple&&this.result_select(a),this.mouse_on_container=!1;break;case 13:a.preventDefault();break;case 38:a.preventDefault(),this.keyup_arrow();break;case 40:a.preventDefault(),this.keydown_arrow()}},Chosen.prototype.search_field_scale=function(){var a,b,c,d,e,f,g,h;if(this.is_multiple){for(b=0,f=0,d="position:absolute; left: -1000px; top: -1000px; display:none;",e=["font-size","font-style","font-weight","font-family","line-height","text-transform","letter-spacing"],g=0,h=e.length;h>g;g++)c=e[g],d+=c+":"+this.search_field.getStyle(c)+";";return a=new Element("div",{style:d}).update(this.search_field.value.escapeHTML()),document.body.appendChild(a),f=Element.measure(a,"width")+25,a.remove(),this.f_width||(this.f_width=this.container.getWidth()),f>this.f_width-10&&(f=this.f_width-10),this.search_field.setStyle({width:f+"px"})}},Chosen}(AbstractChosen),a.Chosen=Chosen}.call(this);